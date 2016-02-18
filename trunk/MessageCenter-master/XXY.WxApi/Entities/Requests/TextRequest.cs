using System;
using System.Xml.Serialization;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Requests {

    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    [RequestType(RequestTypes.Text)]
    public class TextRequest : BaseRequest {
        public string Content {
            get;
            set;
        }
    }
}
