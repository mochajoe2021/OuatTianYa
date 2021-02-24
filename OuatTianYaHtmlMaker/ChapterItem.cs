using System.Collections.Generic;

namespace OuatTianYaHtmlMaker
{
    public class ChapterItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Time
        {
            get; set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Text
        {
            get; set;
        }
        /// <summary>
        /// 读者们
        /// </summary>
        public List<ReadersItem> Readers
        {
            get; set;
        }
    }


}
