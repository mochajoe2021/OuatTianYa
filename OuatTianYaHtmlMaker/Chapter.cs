using System.Collections.Generic;

namespace OuatTianYaHtmlMaker
{
    public class Chapter
    {
        /// <summary>
        ///
        /// </summary>
        public string Number
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
        public List<Reader> Readers
        {
            get; set;
        }
    }
}