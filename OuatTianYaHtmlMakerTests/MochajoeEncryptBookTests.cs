using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using OuatTianYaHtmlMaker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker.Tests
{
    [TestClass()]
    public class MochajoeEncryptBookTests
    {
        [TestMethod()]
        public void MochajoeEncryptBookTest()
        {
            string buffer = System.IO.File.ReadAllText("MochajoeEncryptBook.json");
            MochajoeEncryptBook mjeb = JsonConvert.DeserializeObject<MochajoeEncryptBook>(buffer);

            Assert.IsNotNull(mjeb);
            Assert.IsNotNull(mjeb.AuthorName);
            Assert.IsNotNull(mjeb.Title);
            Assert.AreEqual("三语沫", mjeb.AuthorName);
            Assert.IsTrue(mjeb.EChapters.Count > 0);
            Assert.IsNotNull(mjeb.EChapters[0].Etext);
            Assert.IsNotNull(mjeb.EChapters[0].Esign);
            Assert.IsNotNull(mjeb.EChapters[1].Ekey);
        }
    }
}