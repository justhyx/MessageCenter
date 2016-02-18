using Newtonsoft.Json;

namespace XXY.WxApi.Entities.Messages {
    public class VideoMessage : Message {

        [JsonProperty("thumb_media_id")]
        public string ThumbMediaID {
            get;
            set;
        }

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


        public override MsgTypes MsgType {
            get {
                return MsgTypes.Video;
            }
        }
    }
}
