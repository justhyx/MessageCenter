using Newtonsoft.Json;
using System.Collections.Generic;
using XXY.WxApi.Converters;

namespace XXY.WxApi.Entities.Menus {
    /// <summary>
    /// 带子菜单的菜单
    /// </summary>
    public class SubMenus : BaseMenu {
        [JsonProperty("sub_button", ItemConverterType = typeof(MenuConverter))]
        public List<ButtonMenu> SubButtons {
            get;
            set;
        }
    }
}
