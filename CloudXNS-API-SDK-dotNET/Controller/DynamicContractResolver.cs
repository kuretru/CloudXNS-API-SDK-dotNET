using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kuretru.CloudXNSAPI.Controller
{
    public class DynamicContractResolver : DefaultContractResolver
    {
        string[] props = null;

        /// <summary>
        /// 用于动态决定序列化的成员
        /// </summary>
        /// <param name="props">传入需要序列化的字段</param>
        public DynamicContractResolver(string[] props)
        {
            this.props = props;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            return list.Where(p => props.Contains(p.PropertyName)).ToList();
        }
    }
}
