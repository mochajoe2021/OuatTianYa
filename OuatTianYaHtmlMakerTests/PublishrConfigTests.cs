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
    public class PublishrConfigTests
    {
        [TestMethod()]
        public void PublishrConfigTest()
        {
            string author = "三语沫";
            PublishrConfig cfg = new PublishrConfig(author);
            Assert.IsNotNull(cfg);
            Assert.IsNotNull(cfg.Template_Info);
            Assert.IsNotNull(cfg.Template_Chapters);
            Assert.IsNotNull(cfg.Template_Html_Html);
            Assert.IsNotNull(cfg.Template_Html_Script);
            Assert.IsNotNull(cfg.Template_Html_CSS);
            Assert.IsNotNull(cfg.Template_Html_Body);
            Assert.IsTrue(cfg.Template_Info.Contains(author));
        }
    }
}