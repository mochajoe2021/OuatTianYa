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
       
        public Publishr()
        {
            string s1 = Resource1.String1;
            string s2 = OuatTools.U2A(s1);
            string s3 = OuatTools.A2U(s2);
            Console.WriteLine(s1==s3);
          
            Re = JsonConvert.DeserializeObject<ReadingEnvironment>(OuatTools.U2A(Resource1.String1));
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
