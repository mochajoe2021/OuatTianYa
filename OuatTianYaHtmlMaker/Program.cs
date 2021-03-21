namespace OuatTianYaHtmlMaker
{
    /// <summary>
    /// v1.0
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            PublishrConfig cfg = new PublishrConfig("三语沫");
            Publisher ps = new Publisher(cfg);
            ps.MakeHtml();
        }
    }
}