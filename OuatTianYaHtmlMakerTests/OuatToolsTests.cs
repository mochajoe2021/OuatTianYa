using Microsoft.VisualStudio.TestTools.UnitTesting;

using OuatTianYaHtmlMaker.OuatTianYaHtmlMaker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker.OuatTianYaHtmlMaker.Tests
{
    [TestClass()]
    public class OuatToolsTests
    {
        string[] config;
        string[] Writer = new string[2];
        string[] Reader = new string[2];
        string TestText;
        [TestInitialize]
        public void Begin()
        {
            config = System.IO.File.ReadAllLines("TestConfig.user");
            Writer[0] = "OUATianya-Writer-Test.cer";
            Writer[1] = "OUATianya-Writer-Test.pfx";
            Reader[0] = "OUATianya-Writer-Test.cer";
            Reader[1] = "OUATianya-Writer-Test.pfx";
            TestText = "---This is a Test.这是一个测试。---";

        }
        [TestMethod()]
        public void EncryptDataTest()
        {
            string edatas = OuatTools.EncryptData(TestText, Reader[0]);
            Assert.IsNotNull(edatas);
        }

        [TestMethod()]
        public void DecryptDataTest()
        {
            string edatas = OuatTools.EncryptData(TestText, Reader[0]);
            string datas = OuatTools.DecryptData(edatas, Writer[1], config[0]);
            Assert.IsNotNull(edatas);
            Assert.IsNotNull(datas);
            Assert.AreEqual(TestText, datas);
        }

        [TestMethod()]
        public void SignDataTest()
        {
            string sign = OuatTools.SignData(TestText, Writer[1], config[0]);
            Assert.IsNotNull(sign);
        }

        [TestMethod()]
        public void VerifyDataTest()
        {
            string sign = OuatTools.SignData(TestText, Writer[1], config[0]);
            bool verify = OuatTools.VerifyData(TestText, Writer[0], sign);
            Assert.IsNotNull(sign);
            Assert.IsTrue(verify);
        }


    }
}