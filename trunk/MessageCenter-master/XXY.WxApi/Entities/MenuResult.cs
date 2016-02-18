using Newtonsoft.Json;
using System.Collections.Generic;
using XXY.WxApi.Converters;
using XXY.WxApi.Entities.Menus;

namespace XXY.WxApi.Entities {

    public class MenuResult : BaseResult {

        [JsonProperty("menu")]
        public Menu Menu {
            get;
            set;
        }

    }

    public class Menu {

        [JsonProperty("button", ItemConverterType = typeof(MenuConverter))]
        public List<BaseMenu> Buttons {
            get;
            set;
        }

    }
}
