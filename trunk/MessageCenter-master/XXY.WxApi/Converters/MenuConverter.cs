using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XXY.Common.Extends;
using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Menus;

namespace XXY.WxApi.Converters {
    public class MenuConverter : JsonCreationConverter<BaseMenu> {

        private static readonly Dictionary<MenuButtonTypes, Type> Types = null;

        static MenuConverter() {
            Types = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    t.GetCustomAttribute<MenuTypeAttribute>(false) != null &&
                    typeof(ButtonMenu).IsAssignableFrom(t)
                    )
                .Select(t => new {
                    BT = t.GetCustomAttribute<MenuTypeAttribute>(false).Type,
                    T = t
                })
                .ToLookup(t => t.BT)
                .ToDictionary(t => t.Key, t => t.First().T);
        }

        protected override BaseMenu Create(Type objectType, JObject jObject) {
            if (this.FieldExists("type", jObject)) {
                var mbt = ((string)((JValue)jObject["type"]).Value).ToEnum<MenuButtonTypes>();
                var type = Types.Get(mbt, null);
                if (type != null) {
                    return (BaseMenu)Activator.CreateInstance(type);
                }
            } else {
                return new SubMenus();
            }
            return null;
        }

        private bool FieldExists(string fieldName, JObject jObject) {
            return jObject[fieldName] != null;
        }
    }
}
