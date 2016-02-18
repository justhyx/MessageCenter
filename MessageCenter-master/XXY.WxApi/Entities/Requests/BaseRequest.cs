using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using XXY.Common.Extends;
using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Requests {

    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    public abstract class BaseRequest {

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName {
            get;
            set;
        }

        /// <summary>
        /// 发送方帐号
        /// </summary>
        public string FromUserName {
            get;
            set;
        }

        /// <summary>
        /// 消息创建时间 （整型） 
        /// 从 1970年到现在的秒数
        /// </summary>
        public long CreateTime {
            get;
            set;
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public RequestTypes MsgType {
            get;
            set;
        }

        /// <summary>
        /// 消息ID,并不是所有消息都有这个值
        /// </summary>
        public long MsgId {
            get;
            set;
        }


        private static readonly Dictionary<string, Type> Types;

        static BaseRequest() {
            Types = Assembly.GetCallingAssembly().GetTypes()
                .Where(t =>
                    t.IsSubclassOf(typeof(BaseRequest))
                    && t.IsPublic
                    && t.GetCustomAttribute<RequestTypeAttribute>() != null
                )
                .Select(t => {
                    var attr = t.GetCustomAttribute<RequestTypeAttribute>();
                    return new {
                        MessageType = attr.MessageType,
                        EventType = attr.EventType,
                        Type = t
                    };
                })
                .ToLookup(t => string.Format("{0},{1}", t.MessageType, t.EventType), t => t.Type)
                .ToDictionary(t => t.Key, t => t.First());
        }

        /// <summary>
        /// 如果没有找到对应的消息类型,返回 null
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public static Type GetMessageType(RequestTypes msgType, EventTypes? eventType) {
            return Types.Get(string.Format("{0},{1}", msgType, eventType), null);
        }
    }
}
