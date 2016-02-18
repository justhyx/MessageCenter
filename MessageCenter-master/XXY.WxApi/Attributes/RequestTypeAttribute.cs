using System;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class RequestTypeAttribute : Attribute {

        public RequestTypes MessageType {
            get;
            private set;
        }

        public EventTypes? EventType {
            get;
            private set;
        }

        public RequestTypeAttribute(RequestTypes msgType) {
            this.MessageType = msgType;
            this.EventType = null;
        }

        public RequestTypeAttribute(RequestTypes msgType, EventTypes eventType) {
            this.MessageType = msgType;
            this.EventType = eventType;
        }

    }
}
