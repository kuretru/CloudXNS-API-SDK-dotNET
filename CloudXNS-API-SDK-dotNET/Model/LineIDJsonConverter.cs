using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// 用于区域列表、ISP列表中的线路ID，自定义Json格式化
    /// </summary>
    internal class LineIDJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string[] data = reader.Value.ToString().Split(',');
            List<int> list = new List<int>(Array.ConvertAll<string, int>(data, s => Convert.ToInt32(s)));
            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<int> list = (List<int>)value;
            string data = string.Join(",", list);
            writer.WriteValue(data);
        }
    }
}
