﻿using System.Collections.Generic;
using System.Linq;
using XXY.Common.Extends;
using XXY.WxApi.Attributes;

namespace XXY.WxApi {
    internal class ParameterHelper {

        public static Dictionary<string, string> GetParams(object obj) {
            var dic = new Dictionary<string, string>();
            var props = obj.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(ParamAttribute), true).Length > 0);
            foreach (var p in props) {
                var pa = (ParamAttribute)p.GetCustomAttributes(typeof(ParamAttribute), true).First();
                var pms = pa.GetParams(obj, p);
                if (pms != null) {
                    foreach (var pm in pms) {
                        dic.Set(pm.Key, pm.Value);
                    }
                }
            }

            return dic;
        }

    }
}
