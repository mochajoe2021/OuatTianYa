using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker
{
    public class Publisher
    {
        public MochajoeEncryptBook Mjebook { get; set; }
        public PublishrConfig Config { get; set; }
        public string RsaPublisher { get; set; }

        public Publisher(PublishrConfig cfg)
        {
            this.Config = cfg;
            RsaPublisher = cfg.RsaPublisher;
            string buffer = cfg.Template_Info;
            Mjebook = JsonConvert.DeserializeObject<MochajoeEncryptBook>(buffer);
        }

        public void MakeHtml()
        {
            string html = Config.Template_Html_Html;

            //html = html.Replace("!!!EBookEBook!!!", Resource1.String1);
            File.WriteAllText($"{Mjebook.AuthorName}-{Mjebook.Title}.html", html);
        }
    }
}