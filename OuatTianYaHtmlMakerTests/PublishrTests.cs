using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker.Tests
{
    [TestClass()]
    public class PublishrTests
    {
        private static string[] config;
        private static readonly string[] Writer = new string[2];
        private static readonly string[] Reader = new string[2];
        private static readonly string author = "三语沫";

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            config = System.IO.File.ReadAllLines("TestConfig.user");
            Writer[0] = "OUATianya-Writer-Test.cer";
            Writer[1] = "OUATianya-Writer-Test.pfx";
            Reader[0] = "OUATianya-Reader-Test.cer";
            Reader[1] = "OUATianya-Reader-Test.pfx";
        }

        [TestMethod()]
        public void PublishrTest()
        {
            Publisher ps = new Publisher(new PublishrConfig("三语沫"));

            Assert.IsNotNull(ps.Mjebook.AuthorName);
            Assert.IsNotNull(ps.Mjebook.Title);
            Assert.AreEqual("三语沫", ps.Mjebook.AuthorName);
        }

        [TestMethod()]
        public void MakeHtmlTest()
        {
            Publisher ps = new Publisher(new PublishrConfig(author));
            Assert.IsNotNull(ps.Mjebook.AuthorName);
            Assert.IsNotNull(ps.Mjebook.Title);
            Assert.AreEqual(author, ps.Mjebook.AuthorName);
        }

        public void MakeMjBookTest()
        {
            Publisher ps = new Publisher(new PublishrConfig(author));

            MochajoeBook mjb = JsonConvert.DeserializeObject<MochajoeBook>(ps.Config.Template_Info);
            mjb.Chapters = new List<Chapter>();
            string source = File.ReadAllText("book.json.user");
            TianyaDatas tdatas = JsonConvert.DeserializeObject<TianyaDatas>(source);
            int readernow;

            readernow = 0;
            int rid, aidnext;
            TianyaData tchapter, treader;

            for (int i = 0; i < tdatas.author.Length; i++)
            {
                Chapter c1 = new Chapter();
                tchapter = tdatas.author[i];

                c1.Number = i.ToString();
                c1.Time = tchapter.time;
                c1.Text = tchapter.txt;

                c1.Readers = new List<Reader>();

                if (i + 1 < tdatas.author.Length)
                {
                    aidnext = int.Parse(tdatas.author[i + 1].dataid);
                }
                else
                {
                    aidnext = int.MaxValue;
                }
                do
                {
                    treader = tdatas.reader[readernow];
                    rid = int.Parse(treader.dataid);
                    if (rid <= aidnext)
                    {
                        Reader r1 = new Reader();
                        r1.Name = treader.name;
                        r1.Time = treader.time;
                        r1.Reply = treader.txt;

                        c1.Readers.Add(r1);
                        readernow++;
                    }
                } while ((rid <= aidnext) && (readernow < tdatas.reader.Length));
                mjb.Chapters.Add(c1);
            }
            mjb.ChaptersNumber = mjb.Chapters.Count.ToString();
            string json = JsonConvert.SerializeObject(mjb);
            File.WriteAllText("mjbook.json", json);

            Assert.IsNotNull(mjb.Chapters);
        }

        [TestMethod()]
        public void MakeEBookTest()
        {
            Publisher ps = new Publisher(new PublishrConfig(author));
            MochajoeEncryptBook ebk = ps.Mjebook;

            Assert.IsNotNull(ebk.AuthorName);
            Assert.IsNotNull(ebk.Title);
            Assert.AreEqual(author, ebk.AuthorName);
            RSA rsaOwner = RSA.Create();
            rsaOwner.ImportFromPem(OuatTools.Utf8Base64(ebk.OwnerPriKey).ToCharArray());
            RSA rsaPublisher = RSA.Create();
            rsaOwner.ImportFromPem(ps.RsaPublisher.ToCharArray());
            string[] texts = { "test1", "test2" };

            ebk.EChapters = new List<EChapter>();

            for (int i = 0; i < texts.Length; i++)
            {
                string eText = OuatTools.RsaSignAesEncryptData(texts[i], author, rsaOwner, rsaPublisher, out string eKeyIv, out string eSign);
                EChapter e1 = new EChapter();
                e1.Etext = eText;
                e1.Esign = eSign;
                e1.Ekey = eKeyIv;
                e1.Number = i.ToString();

                Assert.IsNotNull(e1.Etext);
                Assert.IsNotNull(e1.Esign);
                Assert.IsNotNull(e1.Ekey);

                ebk.EChapters.Add(e1);
                Assert.IsNotNull(ebk.EChapters[i]);
            }
            for (int i = 0; i < ebk.EChapters.Count; i++)
            {
                bool verify = OuatTools.RsaSignAesDecryptData(ebk.EChapters[i].Etext, rsaOwner, rsaPublisher, ebk.EChapters[i].Ekey, ebk.EChapters[i].Esign, out string text2);

                Assert.IsTrue(verify);
                Assert.AreEqual(texts[i], text2);
            }
            string json = ebk.ToString();
            Assert.IsNotNull(json);
        }
    }
}