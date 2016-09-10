using System;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    public class DateTimeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return startTime.AddSeconds((long)(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long t = (((DateTime)value).ToUniversalTime().Ticks - startTime.Ticks) / 10000000;            //除10000000调整为10位
            writer.WriteValue(t);
        }
    }
}
