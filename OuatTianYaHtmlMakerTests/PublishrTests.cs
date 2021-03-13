using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Publishr ps = new Publishr();

            Assert.IsNotNull(ps.MjBook.AuthorName);
            Assert.IsNotNull(ps.MjBook.Title);
            Assert.AreEqual("三语沫", ps.MjBook.AuthorName);
        }

        [TestMethod()]
        public void MakeHtmlTest()
        {
            Publishr ps = new Publishr();
            Assert.IsNotNull(ps.MjBook.AuthorName);
            Assert.IsNotNull(ps.MjBook.Title);
            Assert.AreEqual("三语沫", ps.MjBook.AuthorName);
        }

        [TestMethod()]
        public void MakeEBookTest()
        {
            Publishr ps = new Publishr();
            string[] texts = ps.MjBook.ChaptersJson();
            Assert.IsNotNull(ps.MjBook.AuthorName);
            Assert.IsNotNull(ps.MjBook.Title);
            Assert.AreEqual("三语沫", ps.MjBook.AuthorName);

            for (int i = 0; i < texts.Length; i++)
            {
                string eText = OuatTools.RsaSignAesEncryptData(texts[i], Reader[0], Writer[1], config[0], out string eKeyIv, out string eSign);
                ps.MjEBook.EChapters[i].Etext = eText;
                ps.MjEBook.EChapters[i].Esign = eSign;
                ps.MjEBook.EChapters[i].Ekey = eKeyIv;

                Assert.IsNotNull(ps.MjEBook.EChapters[i].Etext);
                Assert.IsNotNull(ps.MjEBook.EChapters[i].Esign);
                Assert.IsNotNull(ps.MjEBook.EChapters[i].Ekey);

                bool verify = OuatTools.RsaSignAesDecryptData(ps.MjEBook.EChapters[i].Etext, Writer[0], Reader[1], config[1], ps.MjEBook.EChapters[i].Ekey, ps.MjEBook.EChapters[i].Esign, out string text2);

                Assert.IsTrue(verify);
                Assert.AreEqual(texts[i], text2);
            }

            string jsong = ps.MjEBook.ToString();
            Assert.IsNotNull(jsong);

            MochajoeEncryptBook ebk = new Publishr().MjEBook;
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