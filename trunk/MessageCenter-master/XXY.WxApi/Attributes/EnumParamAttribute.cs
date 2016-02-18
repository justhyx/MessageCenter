﻿using System.Collections.Generic;
using System.Linq;

namespace XXY.WxApi.Attributes {
    public class EnumParamAttribute : ParamAttribute {

        public EnumParamAttribute(string name)
            : base(name) {
        }

        public override Dictionary<string, string> GetParams(object obj, System.Reflection.PropertyInfo p) {

            var value = p.GetValue(obj, null);
            SpecifyValueAttribute sValue = null;
            if (value != null)
                sValue = value.GetType()
                    .GetField(value.ToString())
                    .GetCustomAttributes(false)
                    .OfType<SpecifyValueAttribute>().FirstOrDefault();//.Value;

            if (sValue != null)
                value = sValue.Value;

            if (value == null && this.Required)
                return new Dictionary<string, string>(){
                    {this.Name, ""}
                };
            else if (value == null && !this.Required)
                return null;
            else
                return new Dictionary<string, string>(){
                    {this.Name, value.ToString()}
                };
        }

    }
}
