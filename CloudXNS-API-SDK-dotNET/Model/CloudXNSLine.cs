using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// CloudXNS线路信息实体类
    /// </summary>
    public class CloudXNSLine
    {
        private int _id = 0;
        private string _chineseName = string.Empty;
        private string _englishName = string.Empty;
        private List<CloudXNSLine> _childrenList = null;

        /// <summary>
        ///线路ID 
        /// </summary>
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// 中文名称
        /// </summary>
        [JsonProperty(PropertyName = "chinese_name")]
        public string ChineseName
        {
            get
            {
                return _chineseName;
            }

            set
            {
                _chineseName = value;
            }
        }

        /// <summary>
        /// 英文名称
        /// </summary>
        [JsonProperty(PropertyName = "english_name")]
        public string EnglishName
        {
            get
            {
                return _englishName;
            }

            set
            {
                _englishName = value;
            }
        }

        /// <summary>
        /// 下级线路列表
        /// </summary>
        [JsonProperty(PropertyName = "children")]
        public List<CloudXNSLine> Children
        {
            get
            {
                return _childrenList;
            }

            set
            {
                _childrenList = value;
            }
        }

        public override string ToString()
        {
            return _chineseName;
        }
    }
}
