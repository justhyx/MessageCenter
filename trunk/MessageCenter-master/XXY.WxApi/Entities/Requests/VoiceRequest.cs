using System;
using System.Xml.Serialization;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Requests {
    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    [RequestType(RequestTypes.Voice)]
    public class VoiceRequest : BaseRequest {
        public string MediaId {
            get;
            set;
        }

        public string Format {
            get;
            set;
        }

        public string Recognition {
            get;
            set;
        }
    }
}
