using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker
{
    public class TianyaDatas
    {
        public TianyaData[] author;
        public TianyaData[] reader;
    }

    public class TianyaData
    {
        public int no;
        public string type;
        public string txt;
        public string name;
        public string time;
        public string dataid;
        public string infourl;
    }
}