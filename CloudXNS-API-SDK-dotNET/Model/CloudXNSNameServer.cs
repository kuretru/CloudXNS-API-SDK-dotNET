using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// CloudXNS NS服务器信息实体类
    /// </summary>
    public class CloudXNSNameServer
    {
        private int _level = -1;
        private List<string> _nameServers;

        /// <summary>
        /// 域名等级
        /// </summary>
        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        }

        /// <summary>
        /// 对应等级的NS服务器列表
        /// </summary>
        [JsonProperty(PropertyName = "ns_server")]
        public List<string> NameServers
        {
            get
            {
                return _nameServers;
            }

            set
            {
                _nameServers = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Level:{0}", _level);
        }
    }
}
