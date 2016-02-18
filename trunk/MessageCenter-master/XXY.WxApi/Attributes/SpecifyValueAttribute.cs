using System;
using System.Linq;

namespace XXY.WxApi.Attributes {

    /// <summary>
    /// 枚举的指定值
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SpecifyValueAttribute : Attribute {

        public object Value {
            get;
            set;
        }

        public SpecifyValueAttribute(object value) {
            this.Value = value;
        }

        public static object GetSpecifyValue(object value) {
            if (value != null && value.GetType().IsEnum) {
                var attr = value.GetType().GetField(value.ToString())
                    .GetCustomAttributes(false)
                    .OfType<SpecifyValueAttribute>()
                    .FirstOrDefault();

                if (attr != null)
                    return attr.Value;
            }

            return value;
        }
    }
}
