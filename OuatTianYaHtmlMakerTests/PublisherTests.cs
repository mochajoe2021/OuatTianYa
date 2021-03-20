using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker.Tests
{
    [TestClass()]
    public class PublisherTests
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

            Assert.IsNotNull(ps.MjEncryptBook.AuthorName);
            Assert.IsNotNull(ps.MjEncryptBook.Title);
            Assert.AreEqual("三语沫", ps.MjEncryptBook.AuthorName);
        }

        [TestMethod()]
        public void MakeHtmlTest()
        {
            Publisher ps = new Publisher(new PublishrConfig(author));
            Assert.IsNotNull(ps.MjEncryptBook.AuthorName);
            Assert.IsNotNull(ps.MjEncryptBook.Title);
            Assert.AreEqual(author, ps.MjEncryptBook.AuthorName);
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
            Chapter[] cs = new Chapter[3];
            MochajoeEncryptBook ebk = ps.MjEncryptBook;

            Assert.IsNotNull(ebk);
            Assert.IsNotNull(ebk.AuthorName);
            Assert.AreEqual(author, ebk.AuthorName);

            string no;
            for (int i = 0; i < cs.Length; i++)
            {
                no = i.ToString();
                cs[i] = new Chapter
                {
                    Number = no,
                    Text = "text," + no
                };
            }
            string json = ps.MakeEncryptBook(cs);
            Assert.IsNotNull(json);

            ebk = JsonConvert.DeserializeObject<MochajoeEncryptBook>(json);
            Assert.IsNotNull(ebk);
            Assert.IsNotNull(ebk.AuthorName);
            Assert.AreEqual(author, ebk.AuthorName);
            Assert.IsNotNull(ebk.EChapters);
        }
    }
}