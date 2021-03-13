using Newtonsoft.Json;

namespace OuatTianYaHtmlMaker
{
    public class Publishr
    {
        public MochajoeBook MjBook { get; set; }

        public MochajoeEncryptBook MjEBook { get; set; }

        public Publishr()
        {
            string buffer = System.IO.File.ReadAllText("MochajoeBook.json");

            MjBook = JsonConvert.DeserializeObject<MochajoeBook>(buffer);
            buffer = System.IO.File.ReadAllText("MochajoeEncryptBook.json");
            MjEBook = JsonConvert.DeserializeObject<MochajoeEncryptBook>(buffer);
        }

        public void MakeHtml()
        {
            string html = Resource1.String2;

            html = html.Replace("!!!textsjson!!!", Resource1.String1);
            System.IO.File.WriteAllText($"{MjBook.AuthorName}-{MjBook.Title}.html", html);
        }
    }
}