//如果好用，请收藏地址，帮忙分享。
using System.Collections.Generic;

namespace OuatTianYaHtmlMaker
{
    public class Book
    {
        /// <summary>
        /// 三语沫
        /// </summary>
        public string Author
        {
            get; set;
        }
        /// <summary>
        /// [右岸文字]暗恋直女学妹的日子
        /// </summary>
        public string BookTitle
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Introduction
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChaptersNumber
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Producer Producer
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<ChaptersItem> Chapters
        {
            get; set;
        }
    }
}