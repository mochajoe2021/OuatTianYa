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
            string edatas = OuatTools.RSAEncryptData(TestText, Reader[0]);
            Assert.IsNotNull(edatas);
        }

        [TestMethod()]
        public void DecryptDataTest()
        {
            string edatas = OuatTools.RSAEncryptData(TestText, Reader[0]);
            string datas = OuatTools.RSADecryptData(edatas, Writer[1], config[0]);
            Assert.IsNotNull(edatas);
            Assert.IsNotNull(datas);
            Assert.AreEqual(TestText, datas);
        }

        [TestMethod()]
        public void SignDataTest()
        {
            string sign = OuatTools.RSASignData(TestText, Writer[1], config[0]);
            Assert.IsNotNull(sign);
        }

        [TestMethod()]
        public void VerifyDataTest()
        {
            string sign = OuatTools.RSASignData(TestText, Writer[1], config[0]);
            bool verify = OuatTools.RSAVerifyData(TestText, Writer[0], sign);
            Assert.IsNotNull(sign);
            Assert.IsTrue(verify);
        }

        [TestMethod()]
        public void AESEncryptDataTest()
        {
            string text = "!!!这是一个测试。This is a Test.!!!";
            string key = "keyiskey";
            string strIV = "ivisiv";
            string etext = null;
            etext = OuatTools.AESEncryptData(text, key, strIV);
            Assert.IsNotNull(etext);
        }
        [TestMethod()]
        public void AESEncryptData1MTest()
        {
            string text = "!!!这是一个测试。This is a Test.!!!".PadRight(1024 * 1024, 't');
            string key = "keyiskey".PadRight(322, 'k');
            string strIV = "ivisiv".PadRight(166, 'i');
            string etext = null;
            etext = OuatTools.AESEncryptData(text, key, strIV);
            Assert.IsNotNull(etext);
        }

        [TestMethod()]
        public void AESDecryptDataTest()
        {
            string  text = "!!!这是一个测试。This is a Test.!!!";
            string text2 = null;
            string key = "keyiskey";
            string strIV = "ivisiv";
            string etext = null;
            etext = OuatTools.AESEncryptData(text, key, strIV);
            text2= OuatTools.AESDecryptData(etext, key, strIV);
            Assert.IsNotNull(etext);
            Assert.IsNotNull(text2);
            Assert.AreEqual(text, text2);
        }
        [TestMethod()]
        public void AESDecryptData1MTest()
        {
            string text = "!!!这是一个测试。This is a Test.!!!".PadRight(1024 * 1024, '测');
            string text2 = null;
            string key = "keyiskey";
            string strIV = "ivisiv";
            string etext = null;
            etext = OuatTools.AESEncryptData(text, key, strIV);
            text2 = OuatTools.AESDecryptData(etext, key, strIV);
            Assert.IsNotNull(etext);
            Assert.IsNotNull(text2);
            Assert.AreEqual(text, text2);
        }
    }
}