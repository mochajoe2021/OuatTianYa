using Microsoft.VisualStudio.TestTools.UnitTesting;

using OuatTianYaHtmlMaker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker.Tests
{
    [TestClass()]
    public class PublishrTests
    {
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
    }
}