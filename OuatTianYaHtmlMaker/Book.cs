//如果好用，请收藏地址，帮忙分享。
using System.Collections.Generic;

namespace OuatTianYaHtmlMaker
{
    public class Book
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get; set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string BookTitle
        {
            get; set;
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction
        {
            get; set;
        }
        /// <summary>
        /// 总章节数
        /// </summary>
        public string ChaptersNumber
        {
            get; set;
        }
        /// <summary>
        /// 制作人
        /// </summary>
        public Producer Producer
        {
            get; set;
        }
        /// <summary>
        /// 全部章节
        /// </summary>
        public List<ChaptersItem> Chapters
        {
            get; set;
        }
    }
}