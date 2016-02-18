using Newtonsoft.Json;

namespace XXY.WxApi.Entities.Menus {
    public abstract class BaseMenu {
        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }
    }
}
