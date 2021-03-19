using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker
{
    public class Publishr
    {
        public MochajoeEncryptBook mjebook { get; set; }
        public PublishrConfig config;

        public Publishr(PublishrConfig cfg)
        {
            this.config = cfg;
            string buffer = cfg.Template_Info;
            mjebook = JsonConvert.DeserializeObject<MochajoeEncryptBook>(buffer);
        }

        public void MakeHtml()
        {
            string html = config.Template_Html_Html;

            //html = html.Replace("!!!textsjson!!!", Resource1.String1);
            System.IO.File.WriteAllText($"{mjebook.AuthorName}-{mjebook.Title}.html", html);
        }
    }
}