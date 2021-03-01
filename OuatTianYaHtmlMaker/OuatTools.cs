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
