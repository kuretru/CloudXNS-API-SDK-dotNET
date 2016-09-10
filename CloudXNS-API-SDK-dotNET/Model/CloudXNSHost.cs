using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// CloudXNS主机记录实体类
    /// </summary>
    public class CloudXNSHost
    {
        private int _id = 0;
        private string _host = string.Empty;
        private int _recordCount = 0;
        private string _domainName = string.Empty;

        /// <summary>
        /// 主机记录ID
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
        /// 主机记录名称
        /// </summary>
        [JsonProperty(PropertyName = "host")]
        public string HostName
        {
            get
            {
                return _host;
            }

            set
            {
                _host = value;
            }
        }

        /// <summary>
        /// 主机记录下的解析记录数
        /// </summary>
        [JsonProperty(PropertyName = "record_num")]
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }

            set
            {
                _recordCount = value;
            }
        }

        /// <summary>
        /// 所属域名
        /// </summary>
        [JsonProperty(PropertyName = "domain_name")]
        public string DomainName
        {
            get
            {
                return _domainName;
            }

            set
            {
                _domainName = value;
            }
        }

        public override string ToString()
        {
            return _domainName;
        }
    }
}
