using System;
using XXY.WxApi.Entities.Menus;

namespace XXY.WxApi.Attributes {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MenuTypeAttribute : Attribute {

        public MenuButtonTypes Type {
            get;
            private set;
        }

        public MenuTypeAttribute(MenuButtonTypes type) {
            this.Type = type;
        }

    }
}
