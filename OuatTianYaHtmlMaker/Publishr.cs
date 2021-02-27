using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker
{
    public class Publishr
    {
        public ReadingEnvironment Re
        {
            get;
            set;
        }
        private static string U2A(string theText)
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
        private static string A2U (string theText)
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
        public Publishr()
        {
            string s1 = Resource1.String1;
            string s2 = U2A(s1);
            string s3 = A2U(s2);
            Console.WriteLine(s1==s3);
          
            Re = JsonConvert.DeserializeObject<ReadingEnvironment>(U2A(Resource1.String1));
        }
        public void MakeHtml()
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(Re.Author);
            Console.WriteLine();
            Console.WriteLine(Re.Title);
            Console.WriteLine();
            Console.WriteLine(Re.Chapters[0].Text);
            string html = Resource1.String2;

            html = html.Replace("!!!textsjson!!!", Resource1.String1);
            System.IO.File.WriteAllText($"{Re.Author}-{Re.Title}.html", html);

        }
    }
}
