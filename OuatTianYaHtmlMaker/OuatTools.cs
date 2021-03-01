using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    namespace OuatTianYaHtmlMaker
    {
        internal class OuatTools
        {
            static readonly HashAlgorithmName sha512 = HashAlgorithmName.SHA512;

            public string EncryptData(string text, string pubKeyfile)
            {
                X509Certificate2 x509 = new X509Certificate2(pubKeyfile);

                byte[] datas = Encoding.UTF8.GetBytes(text);
                RSA rsa = x509.GetRSAPublicKey();
                byte[] edatas = rsa.Encrypt(datas, RSAEncryptionPadding.Pkcs1);

                return Convert.ToBase64String(edatas);
            }

            public string DecryptData(string ciphertext, string prvKeyfile, string pw)
            {
                X509Certificate2 x509 = new X509Certificate2(prvKeyfile, pw);
                byte[] edatas = Convert.FromBase64String(ciphertext);
                RSA rsa = x509.GetRSAPrivateKey();
                byte[] datas = rsa.Decrypt(edatas, RSAEncryptionPadding.Pkcs1);

                return Encoding.UTF8.GetString(datas);
            }
            public string SignData(string text, string prvKeyfile, string pw)
            {
                X509Certificate2 x509 = new X509Certificate2(prvKeyfile, pw);
                byte[] datas = Encoding.UTF8.GetBytes(text);
                RSA rsa = x509.GetRSAPrivateKey();
                byte[] sign = rsa.SignData(datas, sha512, RSASignaturePadding.Pkcs1);

                return Convert.ToBase64String(sign);
            }
            public bool VerifyData(string text, string pubKeyfile, string signstr)
            {
                X509Certificate2 x509 = new X509Certificate2(pubKeyfile);
                byte[] datas = Encoding.UTF8.GetString(text);
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

        }
    }

}
