using System;
using System.Xml.Serialization;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Requests {

    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    [RequestType(RequestTypes.Link)]
    public class LinkRequest : BaseRequest {

        public string Title {
            get;
            set;
        }

        public string Description {
            get;
            set;
        }

        public string Url {
            get;
            set;
        }

    }
}
