using System;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// CloudXNS域名解析统计数据实体类
    /// </summary>
    public class CloudXNSDomainStat
    {
        private DateTime _date = DateTime.MinValue;
        private Int64 _value = -1;

        /// <summary>
        /// 时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        /// <summary>
        /// 解析量
        /// </summary>
        public long Value
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

        public override string ToString()
        {
            return string.Format("{0}    {1}", _date.ToString(), _value);
        }
    }
}
