using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker
{


    namespace OuatTianYaHtmlMaker
    {
        public class OuatTools
        {
            static readonly HashAlgorithmName sha512 = HashAlgorithmName.SHA512;

            public static string RSAEncryptData(string text, string pubKeyfile)
            {
                X509Certificate2 x509 = new X509Certificate2(pubKeyfile);

                byte[] datas = Encoding.UTF8.GetBytes(text);
                RSA rsa = (RSA)x509.GetRSAPublicKey();
                byte[] edatas = rsa.Encrypt(datas, RSAEncryptionPadding.Pkcs1);

                return Convert.ToBase64String(edatas);
            }

            public static string RSADecryptData(string ciphertext, string prvKeyfile, string pw)
            {
                X509Certificate2 x509 = new X509Certificate2(prvKeyfile, pw, X509KeyStorageFlags.Exportable);
                byte[] edatas = Convert.FromBase64String(ciphertext);
                RSA rsa = x509.GetRSAPrivateKey();
                RSAParameters para = rsa.ExportParameters(true);
                rsa.ImportParameters(para);
                byte[] datas = rsa.Decrypt(edatas, RSAEncryptionPadding.Pkcs1);
                string ret = Encoding.UTF8.GetString(datas);
                return ret;
            }
            public static string RSASignData(string text, string prvKeyfile, string pw)
            {
                X509Certificate2 x509 = new X509Certificate2(prvKeyfile, pw);
                byte[] datas = Encoding.UTF8.GetBytes(text);
                RSA rsa = x509.GetRSAPrivateKey();
                byte[] sign = rsa.SignData(datas, sha512, RSASignaturePadding.Pkcs1);

                return Convert.ToBase64String(sign);
            }
            public static bool RSAVerifyData(string text, string pubKeyfile, string signstr)
            {
                X509Certificate2 x509 = new X509Certificate2(pubKeyfile);
                byte[] datas = Encoding.UTF8.GetBytes(text);
                byte[] sign = Convert.FromBase64String(signstr);
                RSA rsa = x509.GetRSAPublicKey();
                bool verify = rsa.VerifyData(datas, sign, sha512, RSASignaturePadding.Pkcs1);

                return verify;
            }
            public static string U2A(string theText)
            {
                try
                {
                    string output = string.Empty;
                    if (theText.Contains("&#"))
                    {
                        output = System.Text.RegularExpressions.Regex.Replace(
                        theText,
                        @"&#(?<Value>[a-zA-Z0-9]+);",
                        m =>
                        {
                            return ((char)int.Parse(m.Groups["Value"].Value)).ToString();
                        });
                    }
                    return output;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return "";
            }
            public static string A2U(string theText)
            {
                try
                {
                    string output = string.Empty;
                    StringBuilder sb1 = new StringBuilder();

                    for (int i = 0; i < theText.Length; i++)
                    {
                        sb1.Append($"&#{(int)theText[i]};");
                    }

                    output = sb1.ToString();
                    return output;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return "";
            }


            public static string AESEncryptData(string text, string key, string strIV)
            {
                byte[] datas = Encoding.UTF8.GetBytes(text);
                byte[] bKey = new byte[32];
                Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);
                byte[] bIV = new byte[16];
                Array.Copy(Encoding.UTF8.GetBytes(strIV.PadRight(bIV.Length)), bIV, bIV.Length);

                byte[] edatas = null;
                string ret = null;
                using (Aes Aes = Aes.Create())
                {
                    try
                    {
                        using (MemoryStream Memory = new MemoryStream())
                        {
                            using (CryptoStream Encryptor = new CryptoStream(Memory, Aes.CreateEncryptor(bKey, bIV),
                             CryptoStreamMode.Write))
                            {
                                Encryptor.Write(datas, 0, datas.Length);
                                Encryptor.FlushFinalBlock();
                                edatas = Memory.ToArray();
                            }
                        }
                        ret = Convert.ToBase64String(edatas);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        edatas = null;
                        ret = null;
                    }

                    return ret;
                }
            }

            public static string AESDecryptData(string etext, string key, string strVI)
            {
                byte[] edatas = Convert.FromBase64String(etext);
                byte[] bKey = new byte[32];
                Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);
                byte[] bVI = new byte[16];
                Array.Copy(Encoding.UTF8.GetBytes(strVI.PadRight(bVI.Length)), bVI, bVI.Length);

                byte[] datas = null;
                string ret = null;

                using (Aes Aes = Aes.Create())
                {
                    try
                    {
                        using (MemoryStream Memory = new MemoryStream(edatas))
                        {
                            using (CryptoStream Decryptor = new CryptoStream(Memory, Aes.CreateDecryptor(bKey, bVI), CryptoStreamMode.Read))
                            {
                                using (MemoryStream tempMemory = new MemoryStream())
                                {
                                    byte[] Buffer = new byte[1024];
                                    Int32 readBytes = 0;
                                    while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                                    {
                                        tempMemory.Write(Buffer, 0, readBytes);
                                    }

                                    datas = tempMemory.ToArray();
                                }
                            }
                            ret=Encoding.UTF8.GetString(datas);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        datas = null;
                        ret = null;
                    }

                    return ret;
                }
            }

        }
    }

}
