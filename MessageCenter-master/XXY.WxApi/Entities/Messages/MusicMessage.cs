using Newtonsoft.Json;

namespace XXY.WxApi.Entities.Messages {
    public class MusicMessage : Message {


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

        [JsonProperty("musicurl")]
        public string Url {
            get;
            set;
        }

        /// <summary>
        /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        [JsonProperty("hqmusicurl")]
        public string HQUrl {
            get;
            set;
        }

        [JsonProperty("thumb_media_id")]
        public string ThumbMediaId {
            get;
            set;
        }

        [JsonProperty("media_id")]
        public string MediaID {
            get;
            set;
        }

        public override MsgTypes MsgType {
            get {
                return MsgTypes.Music;
            }
        }
    }
}
