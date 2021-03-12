using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker.Tests
{
    [TestClass()]
    public class MochajoeBookTests
    {
        [TestMethod()]
        public void MochajoeBookTest()
        {
            string buffer = System.IO.File.ReadAllText("MochajoeBook.json");
            MochajoeBook mjbook = JsonConvert.DeserializeObject<MochajoeBook>(buffer);

            mjbook.Chapters[0].Text = "a\\\\/,a,\\a,a,\"a\"{aaa}aa";
            string s1 = JsonConvert.SerializeObject(mjbook.Chapters[0]);

            Assert.IsNotNull(s1);
            Assert.IsNotNull(mjbook.AuthorName);
            Assert.IsNotNull(mjbook.Title);
            Assert.AreEqual("三语沫", mjbook.AuthorName);
        }
    }
}