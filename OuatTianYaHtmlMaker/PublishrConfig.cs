namespace OuatTianYaHtmlMaker
{
    public class PublishrConfig
    {
        public PublishrConfig(string author)
        {
            if (author.Equals("三语沫"))
            {
                Template_Info = OuatTools.Template2String(Resource1.Template_Info);
                Template_Chapters = OuatTools.Template2String(Resource1.Template_Chapters);
                Template_Html_Html = OuatTools.Template2String(Resource1.Template_Html_Html);
                Template_Html_Script = OuatTools.Template2String(Resource1.Template_Html_Script);
                Template_Html_CSS = OuatTools.Template2String(Resource1.Template_Html_CSS);
                Template_Html_Body = OuatTools.Template2String(Resource1.Template_Html_Body);
            }
        }

        public PublishrConfig(string template_Info, string template_Chapters, string template_EncryptChapters, string template_Html_Html, string template_Html_Script, string template_Html_CSS, string template_Html_Body)
        {
            Template_Info = template_Info;
            Template_Chapters = template_Chapters;
            Template_EncryptChapters = template_EncryptChapters;
            Template_Html_Html = template_Html_Html;
            Template_Html_Script = template_Html_Script;
            Template_Html_CSS = template_Html_CSS;
            Template_Html_Body = template_Html_Body;
        }

        public string Template_Info { get; set; }
        public string Template_Chapters { get; set; }
        public string Template_EncryptChapters { get; set; }
        public string Template_Html_Html { get; set; }
        public string Template_Html_Script { get; set; }
        public string Template_Html_CSS { get; set; }
        public string Template_Html_Body { get; set; }
    }
}