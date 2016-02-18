using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XXY.WxApi.Entities.Menus {
    /// <summary>
    /// 
    /// </summary>
    public abstract class ButtonMenu : BaseMenu {

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public abstract MenuButtonTypes Type {
            get;
        }
    }
}
