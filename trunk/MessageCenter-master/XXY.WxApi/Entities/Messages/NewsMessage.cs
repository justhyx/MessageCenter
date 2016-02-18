using Newtonsoft.Json;
using System.Collections.Generic;

namespace XXY.WxApi.Entities.Messages {
    public class NewsMessage : Message {

        [JsonProperty("articles")]
        public List<Article> Articles {
            get;
            set;
        }



        public override MsgTypes MsgType {
            get {
                return MsgTypes.News;
            }
        }
    }
}
