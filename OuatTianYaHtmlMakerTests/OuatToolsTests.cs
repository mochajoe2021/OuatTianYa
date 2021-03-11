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
        string TestText, TestLongText, RSATestLongText;
        [TestInitialize]
        public void Begin()
        {
            config = System.IO.File.ReadAllLines("TestConfig.user");
            Writer[0] = "OUATianya-Writer-Test.cer";
            Writer[1] = "OUATianya-Writer-Test.pfx";
            Reader[0] = "OUATianya-Reader-Test.cer";
            Reader[1] = "OUATianya-Reader-Test.pfx";
            TestText = "---This is a Test.这是一个测试。--- C#";
            TestLongText = "!!!这是一个测试。This is a Test.!!! C#".PadRight(1024 * 1024 + new Random
(DateTime.Now.Millisecond).Next(255), (char)new Random(DateTime.Now.Millisecond).Next(255));
            RSATestLongText = "!!!这是一个测试。This is a Test.!!! C#".PadRight(1024 * 5 + new Random
          (DateTime.Now.Millisecond).Next(255), (char)new Random(DateTime.Now.Millisecond).Next(255));
        }
        [TestMethod()]
        public void RSAEncryptDataTest()
        {
            string edatas = OuatTools.RSAEncryptData(TestText, Reader[0]);
            Assert.IsNotNull(edatas);
        }

        [TestMethod()]
        public void RSADecryptDataTest()
        {
            string edatas = OuatTools.RSAEncryptData(TestText, Reader[0]);
            string datas = OuatTools.RSADecryptData(edatas, Reader[1], config[1]);
            Assert.IsNotNull(edatas);
            Assert.IsNotNull(datas);
            Assert.AreEqual(TestText, datas);
        }

        [TestMethod()]
        public void RSADecryptDataBigTest()
        {
            string edatas = OuatTools.RSAEncryptData(RSATestLongText, Reader[0]);
            string datas = OuatTools.RSADecryptData(edatas, Reader[1], config[1]);
            Assert.IsNotNull(edatas);
            Assert.IsNotNull(datas);
            Assert.AreEqual(RSATestLongText, datas);
        }

        [TestMethod()]
        public void RSASignDataTest()
        {
            string sign = OuatTools.RSASignData(TestText, Writer[1], config[0]);
            Assert.IsNotNull(sign);
        }

        [TestMethod()]
        public void RSAVerifyDataTest()
        {
            string sign = OuatTools.RSASignData(TestLongText, Writer[1], config[0]);
            bool verify = OuatTools.RSAVerifyData(TestLongText, Writer[0], sign);
            Assert.IsNotNull(sign);
            Assert.IsTrue(verify);
        }

        [TestMethod()]
        public void AESEncryptDataTest()
        {
            string key = "keyiskey";
            string strIV = "ivisiv";
            string etext = null;
            etext = OuatTools.AESEncryptData(TestText, key, strIV);
            Assert.IsNotNull(etext);
        }
        [TestMethod()]
        public void AESEncryptData1MTest()
        {
            string text = TestLongText;
            string key = "keyiskey".PadRight(322, 'k');
            string strIV = "ivisiv".PadRight(166, 'i');
            string etext = null;
            etext = OuatTools.AESEncryptData(text, key, strIV);
            Assert.IsNotNull(etext);
        }

        [TestMethod()]
        public void AESDecryptDataTest()
        {
            string text2 ;
            string key = "keyiskey";
            string strIV = "ivisiv";
            string etext = null;
            etext = OuatTools.AESEncryptData(TestText, key, strIV);
            text2 = OuatTools.AESDecryptData(etext, key, strIV);
            Assert.IsNotNull(etext);
            Assert.IsNotNull(text2);
            Assert.AreEqual(TestText, text2);
        }
        [TestMethod()]
        public void AESDecryptData1MTest()
        {
            string text2 = null;
            string key = "keyiskey";
            string strIV = "ivisiv";
            string etext = null;
            etext = OuatTools.AESEncryptData(TestLongText, key, strIV);
            text2 = OuatTools.AESDecryptData(etext, key, strIV);
            Assert.IsNotNull(etext);
            Assert.IsNotNull(text2);
            Assert.AreEqual(TestLongText, text2);
        }

        [TestMethod()]
        public void MakeChapterTest()
        {
            string eText = OuatTools.RsaSignAesEncryptData(TestText, Reader[0], Writer[1], config[0], out string eKeyIv,out string eSign);
            bool verify = OuatTools.RsaSignAesDecryptData(eText, Writer[0], Reader[1], config[1], eKeyIv,eSign ,out string text2);
            
            Assert.IsNotNull(eText);
            Assert.IsNotNull(eKeyIv);
            Assert.IsNotNull(eSign);
            Assert.IsNotNull(text2);
            Assert.IsTrue(verify);
            Assert.AreEqual(TestLongText, text2);
        }
    }
}