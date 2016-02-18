using Newtonsoft.Json;

namespace XXY.WxApi.Entities.Messages {

    public class TextMessage : Message {



        [JsonProperty("content")]
        public string Content {
            get;
            set;
        }

        public override MsgTypes MsgType {
            get {
                return MsgTypes.Text;
            }
        }
    }
}
