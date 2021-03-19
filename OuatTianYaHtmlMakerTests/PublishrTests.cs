using System.Collections.Generic;
using System.IO;

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
            Publishr ps = new Publishr(new PublishrConfig("三语沫"));

            Assert.IsNotNull(ps.mjebook.AuthorName);
            Assert.IsNotNull(ps.mjebook.Title);
            Assert.AreEqual("三语沫", ps.mjebook.AuthorName);
        }

        [TestMethod()]
        public void MakeHtmlTest()
        {
            Publishr ps = new Publishr(new PublishrConfig("三语沫"));
            Assert.IsNotNull(ps.mjebook.AuthorName);
            Assert.IsNotNull(ps.mjebook.Title);
            Assert.AreEqual("三语沫", ps.mjebook.AuthorName);
        }

        [TestMethod()]
        public void MakeMjBookTest()
        {
            string buffer = File.ReadAllText("三语沫_Head_MochajoeBook.json");
            MochajoeBook MjBook = JsonConvert.DeserializeObject<MochajoeBook>(buffer);

            MjBook.Chapters = new List<Chapter>();
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
                MjBook.Chapters.Add(c1);
            }
            MjBook.ChaptersNumber = MjBook.Chapters.Count.ToString();
            string json = JsonConvert.SerializeObject(MjBook);
            File.WriteAllText("mjbook.json", json);

            Assert.IsNotNull(MjBook.Chapters);
        }

        [TestMethod()]
        public void MakeEBookTest()
        {
            string booksource = "MochajoeBook.json";
            string buffer = File.ReadAllText(booksource);
            MochajoeBook MjBook = JsonConvert.DeserializeObject<MochajoeBook>(buffer);

            Assert.IsNotNull(MjBook.AuthorName);
            Assert.IsNotNull(MjBook.Title);
            Assert.AreEqual("三语沫", MjBook.AuthorName);

            string ebooksource = "MochajoeEncryptBook.json.json";
            buffer = File.ReadAllText(ebooksource);
            MochajoeEncryptBook MjEBook = JsonConvert.DeserializeObject<MochajoeEncryptBook>(buffer);

            string[] texts = MjBook.ChaptersJson();

            MjEBook.EChapters = new List<EChapter>();

            for (int i = 0; i < texts.Length; i++)
            {
                string eText = OuatTools.RsaSignAesEncryptData(texts[i], Reader[0], Writer[1], config[0], out string eKeyIv, out string eSign);
                EChapter e1 = new EChapter();
                e1.Etext = eText;
                e1.Esign = eSign;
                e1.Ekey = eKeyIv;
                e1.Number = i.ToString();

                Assert.IsNotNull(e1.Etext);
                Assert.IsNotNull(e1.Esign);
                Assert.IsNotNull(e1.Ekey);

                MjEBook.EChapters.Add(e1);
                Assert.IsNotNull(MjEBook.EChapters[i]);

                bool verify = OuatTools.RsaSignAesDecryptData(MjEBook.EChapters[i].Etext, Writer[0], Reader[1], config[1], MjEBook.EChapters[i].Ekey, MjEBook.EChapters[i].Esign, out string text2);

                Assert.IsTrue(verify);
                Assert.AreEqual(texts[i], text2);
            }

            string json = MjEBook.ToString();
            File.WriteAllText("e" + booksource, json);
            Assert.IsNotNull(json);
        }

        [TestMethod()]
        public void EBookSignTest()
        {
            string[] texts;
            MochajoeEncryptBook ebk = new Publishr(new PublishrConfig("三语沫")).mjebook;
            texts = new string[ebk.EChapters.Count];
            for (int i = 0; i < ebk.EChapters.Count; i++)
            {
                bool verify = OuatTools.RsaSignAesDecryptData(ebk.EChapters[i].Etext, Writer[0], Reader[1], config[1], ebk.EChapters[i].Ekey, ebk.EChapters[i].Esign, out texts[i]);
                Assert.IsTrue(verify);
                Assert.IsNotNull(texts[i]);
            }
        }
    }
}