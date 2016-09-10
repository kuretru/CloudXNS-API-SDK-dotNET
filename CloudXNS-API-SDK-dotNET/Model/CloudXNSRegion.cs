using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// CloudXNS区域信息实体类
    /// </summary>
    public class CloudXNSRegion
    {
        private List<int> _id;
        private string _chineseName = string.Empty;
        private string _englishName = string.Empty;

        /// <summary>
        /// 组成区域的线路ID
        /// </summary>
        [JsonConverter(typeof(LineIDJsonConverter))]
        public List<int> ID
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
        /// 区域中文名称
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
        /// 区域英文名称
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

        public override string ToString()
        {
            return _chineseName;
        }
    }
}
