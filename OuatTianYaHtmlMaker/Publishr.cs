using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker
{
    public class Publishr
    {
        public MochajoeEncryptBook MjEBook { get; set; }

        public Publishr(string EBooksource = "MochajoeEncryptBook.json")
        {
            string buffer = File.ReadAllText(EBooksource);
            MjEBook = JsonConvert.DeserializeObject<MochajoeEncryptBook>(buffer);
        }

        public void MakeHtml()
        {
            string html = Resource1.String2;

            html = html.Replace("!!!textsjson!!!", Resource1.String1);
            System.IO.File.WriteAllText($"{MjEBook.AuthorName}-{MjEBook.Title}.html", html);
        }
    }
}