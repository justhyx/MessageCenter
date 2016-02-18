using System;
using System.Xml.Serialization;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Requests {

    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    [RequestType(RequestTypes.Event, EventTypes.Unsubscribe)]
    public class UnsubscribeRequest : BaseRequest {
    }
}
