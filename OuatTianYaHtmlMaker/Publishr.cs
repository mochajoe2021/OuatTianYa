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

        public Publishr()
        {
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
