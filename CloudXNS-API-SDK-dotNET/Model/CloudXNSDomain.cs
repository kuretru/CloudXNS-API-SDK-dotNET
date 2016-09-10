using System;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// CloudXNS域名信息实体类
    /// </summary>
    public class CloudXNSDomain
    {
        private int _id = 0;
        private string _domain = string.Empty;
        private string _status = string.Empty;
        private string _takeOverStatus = string.Empty;
        private int _level = 0;
        private DateTime _createTime = DateTime.MinValue;
        private DateTime _updateTime = DateTime.MinValue;
        private int _ttl = 0;

        /// <summary>
        /// 域名ID
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
        /// 域名名称
        /// </summary>
        [JsonProperty(PropertyName = "domain")]
        public string DomainName
        {
            get
            {
                return _domain;
            }

            set
            {
                _domain = value;
            }
        }

        /// <summary>
        /// 域名状态
        /// </summary>
        public string Status
        {
            get
            {
                string result = string.Empty;
                if (_status == "ok")
                {
                    result = "正常";
                }
                else if (_status == "userlock")
                {
                    result = "用户锁定";
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
        /// 域名接管状态
        /// </summary>
        [JsonProperty(PropertyName = "take_over_status")]
        public string TakeOverStatus
        {
            get
            {
                string result = string.Empty;
                if (_takeOverStatus == "ok")
                {
                    result = "已接管";
                }
                else if (_takeOverStatus == "no")
                {
                    result = "未接管";
                }
                else
                {
                    result = string.Format("未知：{0}", _takeOverStatus);
                }
                return result;
            }

            set
            {
                _takeOverStatus = value.ToLower();
            }
        }

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

        [JsonProperty(PropertyName = "create_time")]
        /// <summary>
        /// 创建时间
        /// </summary>
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

        [JsonProperty(PropertyName = "update_time")]
        /// <summary>
        /// 更新时间
        /// </summary>
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
        /// 域名默认TTL
        /// </summary>
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

        public override string ToString()
        {
            return _domain;
        }
    }
}
