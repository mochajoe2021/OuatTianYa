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
            Re = JsonConvert.DeserializeObject<ReadingEnvironment>(Resource1.String1);
        }
        public void  MakeHtml()
        {
            Console.WriteLine(Re.Author);
            Console.WriteLine();
            Console.WriteLine(Re.Title);
            Console.WriteLine();
            Console.WriteLine(Re.Chapters[0].Text);
            string html = Resource1.String2;
            html = html.Replace("{0}", Re.Author);
            html = html.Replace("{1}", Re.Title);
            html = html.Replace("{2}",Re.Chapters[0].Text);
            System.IO.File.WriteAllText($"{Re.Author}-{Re.Title}.html", html);

        }
    }
}
