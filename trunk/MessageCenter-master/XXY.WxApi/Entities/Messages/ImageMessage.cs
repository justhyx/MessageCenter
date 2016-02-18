using Newtonsoft.Json;

namespace XXY.WxApi.Entities.Messages {

    [JsonObject(Title = "image")]
    public class ImageMessage : Message {


        /// <summary>
        /// 发送的图片/语音/视频的媒体ID
        /// </summary>
        [JsonProperty("media_id")]
        public string MediaID {
            get;
            set;
        }

        public override MsgTypes MsgType {
            get {
                return MsgTypes.Image;
            }
        }
    }
}
