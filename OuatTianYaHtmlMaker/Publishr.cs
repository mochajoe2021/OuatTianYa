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
        public MochajoeBook MjBook
        {
            get;
            set;
        }      

        public Publishr()
        {
           string buffer= System.IO.File.ReadAllText("MochajoeBook.json");
            MjBook = JsonConvert.DeserializeObject<MochajoeBook>(buffer);
        }
        public void MakeHtml()
        {
            string html = Resource1.String2;

            html = html.Replace("!!!textsjson!!!", Resource1.String1);
            System.IO.File.WriteAllText($"{MjBook.AuthorName}-{MjBook.Title}.html", html);

        }
    }
}
