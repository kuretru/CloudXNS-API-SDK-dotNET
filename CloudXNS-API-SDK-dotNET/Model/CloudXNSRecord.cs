using System;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// CloudXNS解析记录实体类
    /// </summary>
    public class CloudXNSRecord
    {
        private int _recordID = -1;
        private int _hostID = -1;
        private string _host = string.Empty;
        private int _lineID = -1;
        private string _lineChinese = string.Empty;
        private string _lineEnglish = string.Empty;
        private string _mx = null;
        private string _value = string.Empty;
        private string _spareValue = string.Empty;
        private int _ttl = -1;
        private string _recordType = string.Empty;
        private string _status = string.Empty;
        private DateTime _createTime = DateTime.MinValue;
        private DateTime _updateTime = DateTime.MinValue;
        private int _domainID = -1;
        private string _domainName = string.Empty;

        /// <summary>
        /// 解析记录ID
        /// </summary>
        [JsonProperty(PropertyName = "record_id")]
        public int RecordID
        {
            get
            {
                return _recordID;
            }

            set
            {
                _recordID = value;
            }
        }

        /// <summary>
        /// 主机记录ID
        /// </summary>
        [JsonProperty(PropertyName = "host_id")]
        public int HostID
        {
            get
            {
                return _hostID;
            }

            set
            {
                _hostID = value;
            }
        }

        /// <summary>
        /// 主机记录名
        /// </summary>
        [JsonProperty(PropertyName = "host")]
        public string Host
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
        /// 线路ID
        /// </summary>
        [JsonProperty(PropertyName = "line_id")]
        public int LineID
        {
            get
            {
                return _lineID;
            }

            set
            {
                _lineID = value;
            }
        }

        /// <summary>
        /// 中文名称
        /// </summary>
        [JsonProperty(PropertyName = "line_zh")]
        public string LineChinese
        {
            get
            {
                return _lineChinese;
            }

            set
            {
                _lineChinese = value;
            }
        }

        /// <summary>
        /// 英文名称
        /// </summary>
        [JsonProperty(PropertyName = "line_en")]
        public string LineEnglish
        {
            get
            {
                return _lineEnglish;
            }

            set
            {
                _lineEnglish = value;
            }
        }

        /// <summary>
        /// 邮件交换机优先级
        /// </summary>
        [JsonProperty(PropertyName = "mx")]
        public string MX
        {
            get
            {
                return _mx;
            }

            set
            {
                _mx = value;
            }
        }

        /// <summary>
        /// 记录值
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// 备记录
        /// </summary>
        [JsonProperty(PropertyName = "spare_value")]
        public string SpareValue
        {
            get
            {
                return _spareValue;
            }

            set
            {
                _spareValue = value;
            }
        }

        /// <summary>
        /// TTL生存时间
        /// </summary>
        [JsonProperty(PropertyName = "ttl")]
        public int TTL
        {
            get
            {
                return _ttl;
            }

            set
            {
                _ttl = value;
            }
        }

        /// <summary>
        /// 记录类型
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string RecordType
        {
            get
            {
                return _recordType;
            }

            set
            {
                _recordType = value;
            }
        }

        /// <summary>
        /// 记录状态
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status
        {
            get
            {
                string result = string.Empty;
                if (_status == "ok")
                {
                    result = "已生效";
                }
                else if (_status == "userstop")
                {
                    result = "暂停";
                }
                else
                {
                    result = string.Format("未知：{0}", _status);
                }
                return result;
            }

            set
            {
                _status = value.ToLower();
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty(PropertyName = "create_time")]
        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }

            set
            {
                _createTime = value;
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(PropertyName = "update_time")]
        public DateTime UpdateTime
        {
            get
            {
                return _updateTime;
            }

            set
            {
                _updateTime = value;
            }
        }

        /// <summary>
        /// 域名ID
        /// </summary>
        [JsonProperty(PropertyName = "domain_id")]
        public int DomainID
        {
            get
            {
                return _domainID;
            }

            set
            {
                _domainID = value;
            }
        }

        /// <summary>
        /// 完整域名，仅在更新解析记录时有效
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
            return _host;
        }
    }
}
