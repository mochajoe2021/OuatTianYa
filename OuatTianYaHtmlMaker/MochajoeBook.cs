using System.Collections.Generic;

namespace OuatTianYaHtmlMaker
{
    public class MochajoeBook
    {
        public MochajoeBook()
        {
        }

        /// <summary>
        /// 作者
        /// </summary>
        public string AuthorName
        {
            get; set;
        }

        /// <summary>
        /// 作者公钥
        /// </summary>
        public string AuthorPubKey
        {
            get; set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
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
        /// 全部章节
        /// </summary>
        public List<Chapter> Chapters
        {
            get; set;
        }

        public string[] ChaptersJson()
        {
            Chapter[] cps = Chapters.ToArray();
            string[] ret = new string[cps.Length];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Newtonsoft.Json.JsonConvert.SerializeObject(cps[i]);
            }
            return ret;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        /// <summary>
        ///
        /// </summary>
        public string ProducerName
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string ProductionSoftware
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string OwnerName
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string OwnerPriKey
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Config
        {
            get; set;
        }
    }
}