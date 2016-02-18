using System;
using System.Xml.Serialization;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Requests {

    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    [RequestType(RequestTypes.Event, EventTypes.Subscribe)]
    public class SubscribeRequest : EventRequest {

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket {
            get;
            set;
        }
    }
}
