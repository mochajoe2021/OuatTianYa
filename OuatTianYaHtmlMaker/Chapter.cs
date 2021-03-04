//如果好用，请收藏地址，帮忙分享。
using System.Collections.Generic;

namespace OuatTianYaHtmlMaker
{
    public class Chapter
    {
        /// <summary>
        /// 
        /// </summary>
        public string No
        {
            get; set;
        }
        /// <summary>
        /// 初见
        /// </summary>
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// 2021年3月4日09点41分
        /// </summary>
        public string Time
        {
            get; set;
        }
        /// <summary>
        /// 内容1
        /// </summary>
        public string Text
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<ReadersItem2> Readers
        {
            get; set;
        }
    }
}