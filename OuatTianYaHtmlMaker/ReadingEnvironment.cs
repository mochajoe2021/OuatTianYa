using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuatTianYaHtmlMaker
{

    public class ReadingEnvironment
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get; set;
        }
        /// <summary>
        /// 文章名称
        /// </summary>
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// 总章节
        /// </summary>
        public int ChapterCount
        {
            get; set;
        }
        /// <summary>
        /// 章节
        /// </summary>
        public List<ChapterItem> Chapters
        {
            get; set;
        }
       
    }
}
