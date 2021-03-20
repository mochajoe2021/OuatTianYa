using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Security.Cryptography;

namespace OuatTianYaHtmlMaker
{
    public class Publisher
    {
        public MochajoeEncryptBook MjEncryptBook { get; set; }
        public PublishrConfig Config { get; set; }
        public string RsaPublisher { get; set; }

        public Publisher(PublishrConfig cfg)
        {
            this.Config = cfg;
            RsaPublisher = cfg.RsaPublisher;
            string buffer = cfg.Template_Info;
            MjEncryptBook = JsonConvert.DeserializeObject<MochajoeEncryptBook>(buffer);
        }

        public void MakeHtml()
        {
            string html = Config.Template_Html_Html;
            string ebook;
            string body = Config.Template_Html_Body;

            html = html.Replace("<script></script>", Config.Template_Html_Script);
            html = html.Replace("<style></style>", Config.Template_Html_CSS);
            html = html.Replace("<title></title>", $"<title>{MjEncryptBook.AuthorName}-{MjEncryptBook.Title}</title>");

            ebook = MakeEncryptBook();
            body = body.Replace("!!!EBookEBook!!!", ebook);
            html = html.Replace("<Body></Body>", body);
            File.WriteAllText($"{MjEncryptBook.AuthorName}-{MjEncryptBook.Title}.html", html);
        }

        public string MakeEncryptBook()
        {
            Chapter[] cs = JsonConvert.DeserializeObject<Chapter[]>(Config.Template_Chapters);
            return MakeEncryptBook(cs);
        }

        public string MakeEncryptBook(Chapter[] chapters)
        {
            RSA rsaOwner, rsaPublisher;
            string json;
            bool verify;
            MochajoeEncryptBook ebk;
            EChapter e1;
            string eText;

            ebk = MjEncryptBook;
            rsaOwner = RSA.Create();
            rsaOwner.ImportFromPem(OuatTools.Utf8Base64(ebk.OwnerPriKey).ToCharArray());

            rsaPublisher = RSA.Create();
            rsaPublisher.ImportFromPem(RsaPublisher.ToCharArray());

            ebk.EChapters = new List<EChapter>();

            for (int i = 0; i < chapters.Length; i++)
            {
                eText = OuatTools.RsaSignAesEncryptData(JsonConvert.SerializeObject(chapters[i]), ebk.AuthorName, rsaOwner, rsaPublisher, out string eKeyIv, out string eSign);

                e1 = new EChapter
                {
                    Etext = eText,
                    Esign = eSign,
                    Ekey = eKeyIv,
                    Number = i.ToString()
                };

                ebk.EChapters.Add(e1);
            }

            for (int i = 0; i < ebk.EChapters.Count; i++)
            {
                verify = OuatTools.RsaSignAesDecryptData(ebk.EChapters[i].Etext, rsaOwner, rsaPublisher, ebk.EChapters[i].Ekey, ebk.EChapters[i].Esign, out string text2);
                if (!verify)
                    throw new Exception("EChapters error.");
            }

            json = ebk.ToString();
            return json;
        }
    }
}