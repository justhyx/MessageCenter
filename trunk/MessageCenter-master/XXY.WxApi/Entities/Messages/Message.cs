using Newtonsoft.Json;

namespace XXY.WxApi.Entities.Messages {
    public abstract class Message {

        [JsonIgnore]
        public abstract MsgTypes MsgType {
            get;
        }

    }
}
