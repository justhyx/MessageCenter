using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Converters {
    public class SpecifyValueConverter : JsonConverter {

        public override bool CanConvert(Type objectType) {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (objectType.IsEnum) {
                var fi = objectType.GetFields(BindingFlags.Static | BindingFlags.Public)
                    .FirstOrDefault(f => {
                        var attr = f.GetCustomAttribute<SpecifyValueAttribute>();
                        return attr != null && attr.Value.ToString().Equals(reader.Value.ToString(), StringComparison.OrdinalIgnoreCase);
                    });

                return fi.GetValue(existingValue);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var e = (Enum)value;
            var p = e.GetType().GetField(e.ToString(), BindingFlags.Static | BindingFlags.Public);
            if (p != null) {
                var attr = p.GetCustomAttribute<SpecifyValueAttribute>();
                if (attr != null)
                    value = attr.Value;
            }

            writer.WriteValue(value);
        }
    }
}
