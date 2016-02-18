using Newtonsoft.Json;
using XXY.WxApi.Converters;

namespace XXY.WxApi.Entities {
    public class User : BaseResult {

        [JsonProperty("subscribe")]
        public bool IsSubscribed {
            get;
            set;
        }

        [JsonProperty("openid")]
        public string OpenID {
            get;
            set;
        }

        [JsonProperty("nickname")]
        public string Nickname {
            get;
            set;
        }

        [JsonProperty("sex")]
        public Sex Sex {
            get;
            set;
        }


        [JsonProperty("city")]
        public string City {
            get;
            set;
        }

        [JsonProperty("country")]
        public string Country {
            get;
            set;
        }

        [JsonProperty("province")]
        public string Province {
            get;
            set;
        }

        [JsonProperty("language"),JsonConverter(typeof(SpecifyValueConverter))]
        public Langs Lang {
            get;
            set;
        }

        [JsonProperty("headimgurl")]
        public string HeadImgUrl {
            get;
            set;
        }

        [JsonProperty("subscribe_time")]
        public long SubscribeTime {
            get;
            set;
        }

        public string Unionid {
            get;
            set;
        }
    }
}
