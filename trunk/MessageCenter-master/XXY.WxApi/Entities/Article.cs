using Newtonsoft.Json;

namespace XXY.WxApi.Entities {
    public class Article {

        [JsonProperty("title")]
        public string Title {
            get;
            set;
        }

        [JsonProperty("description")]
        public string Description {
            get;
            set;
        }

        [JsonProperty("url")]
        public string Url {
            get;
            set;
        }

        [JsonProperty("picurl")]
        public string PicUrl {
            get;
            set;
        }
    }
}
