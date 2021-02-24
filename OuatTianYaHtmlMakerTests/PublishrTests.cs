using NUnit.Framework;

using OuatTianYaHtmlMaker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker.Tests
{
    [TestFixture()]
    public class PublishrTests
    {
        [Test()]
        public void PublishrTest()
        {
            Publishr ps = new Publishr();
            Assert.NotNull(ps.Re.Author);
            Assert.NotNull(ps.Re.Title);
            Assert.AreEqual("三语沫", ps.Re.Author);
            
        }
    }
}