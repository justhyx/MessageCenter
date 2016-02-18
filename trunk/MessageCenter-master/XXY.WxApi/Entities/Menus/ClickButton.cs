using Newtonsoft.Json;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Menus {

    /// <summary>
    /// Click 菜单
    /// </summary>
    [MenuType(MenuButtonTypes.click)]
    public class ClickButton : ButtonMenu {
        [JsonProperty("key")]
        public string Key {
            get;
            set;
        }

        public override MenuButtonTypes Type {
            get {
                return MenuButtonTypes.click;
            }
        }
    }
}
