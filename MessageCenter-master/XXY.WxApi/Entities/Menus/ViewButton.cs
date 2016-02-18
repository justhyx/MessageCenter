using Newtonsoft.Json;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Menus {

    /// <summary>
    /// Url菜单
    /// </summary>
    [MenuType(MenuButtonTypes.view)]
    public class ViewButton : ButtonMenu {
        [JsonProperty("url")]
        public string Url {
            get;
            set;
        }


        public override MenuButtonTypes Type {
            get {
                return MenuButtonTypes.view;
            }
        }
    }
}
