using System;
using System.Xml.Serialization;

namespace XXY.WxApi.Entities {
    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    public class Reply {

        public string ToUserName {
            get;
            set;
        }

        public string FromUserName {
            get;
            set;
        }

        /// <summary>
        /// 回复的消息
        /// </summary>
        public string Content {
            get;
            set;
        }

        public long CreateTime {
            get;
            set;
        }

        public string MsgType {
            get;
            set;
        }

        public long MsgId {
            get;
            set;
        }

        public Reply() {
            this.MsgId = DateTime.Now.Ticks;
            this.MsgType = "text";
            this.CreateTime = DateTime.Now.ToUnixTimestamp();
        }
    }
}
