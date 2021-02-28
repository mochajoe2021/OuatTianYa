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
        private static readonly string pubkeyfile = "OUATianya-MochaJoe.cer";
        private static readonly string pubkeypw = "";
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
        public static void PublicKeyPKCS1File(string file= "MochaJoePKCS#1.txt")
        {
            X509Certificate2 x509 = new X509Certificate2(pubkeyfile, Encoding.Unicode.GetString( Convert.FromBase64String(pubkeypw)) );
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendLine("-----BEGIN RSA PUBLIC KEY-----");
            sb1.AppendLine(Convert.ToBase64String(((RSA)x509.PublicKey.Key).ExportRSAPublicKey()));
            sb1.AppendLine("-----END RSA PUBLIC KEY-----");
            System.IO.File.WriteAllText(file, sb1.ToString());
        }
    }
}
