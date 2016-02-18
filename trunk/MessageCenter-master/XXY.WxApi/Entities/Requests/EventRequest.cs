using System;
using System.Xml.Serialization;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Requests {

    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    [RequestType(RequestTypes.Event)]
    public abstract class EventRequest : BaseRequest {

        /// <summary>
        /// 事件类型
        /// </summary>
        public EventTypes Event {
            get;
            set;
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey {
            get;
            set;
        }
    }
}
