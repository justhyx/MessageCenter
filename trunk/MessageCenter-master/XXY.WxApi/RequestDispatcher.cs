using System;
using System.Collections.Generic;
using System.Reflection;
using XXY.Common.Extends;
using XXY.WxApi.Attributes;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;
using XXY.WxApi.Handlers;

namespace XXY.WxApi {

    /// <summary>
    /// 请求分发
    /// </summary>
    public static class RequestDispatcher {

        /// <summary>
        /// 
        /// </summary>
        private static readonly Dictionary<string, Type> HandlerTypes = new Dictionary<string, Type>();

        private static string GetKey(RequestTypes msgType, EventTypes? evtType) {
            return string.Format("{0},{1}", msgType, evtType);
        }

        /// <summary>
        /// 依赖注入点, 如果设置了, 必须将 Handler 进行依赖注册.
        /// </summary>
        public static Func<Type, object> GetService = null;

        private static void Regist(RequestTypes msgType, EventTypes? eventType, Type handlerType) {
            HandlerTypes.Set(GetKey(msgType, eventType), handlerType);
        }

        /// <summary>
        /// 注册 Request 的处理程序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgtype"></param>
        /// <param name="eventType"></param>
        public static void Regist<T>(RequestTypes msgtype, EventTypes? eventType = null) where T : RequestHandler {
            Regist(msgtype, eventType, typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Regist<T>() where T : RequestHandler {
            var rt = typeof(T).GetCustomAttribute<RequestTypeAttribute>();
            if (rt != null) {
                Regist<T>(rt.MessageType, rt.EventType);
            }
        }


        //static MessageDispatcher() {
        //    var ts = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).ToList();
        //    HandlerTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()) //Assembly.GetEntryAssembly().GetTypes()
        //        .Where(t =>
        //            !t.IsAbstract
        //            && t.IsPublic
        //            && typeof(MessageHandler).IsAssignableFrom(t)
        //            && t.GetCustomAttribute<MessageTypeAttribute>(true) != null
        //        )
        //        .Select(t => {
        //            var attr = t.GetCustomAttribute<MessageTypeAttribute>(true);
        //            return new {
        //                MessageType = attr.MessageType,
        //                EventType = attr.EventType,
        //                Type = t
        //            };
        //        })
        //        .ToLookup(t => GetKey(t.MessageType, t.EventType), t => t.Type)
        //        .ToDictionary(t => t.Key, t => t.First());
        //}

        /// <summary>
        /// 将 request 分发到处理程序中
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Reply Dispatch(string tag, BaseRequest request) {

            var handler = (RequestHandler)GetHandler(request);
            if (handler != null) {
                var reply = handler.Handle(tag, request);
                if (reply != null) {
                    reply.FromUserName = request.ToUserName;
                    reply.ToUserName = request.FromUserName;
                    reply.CreateTime = DateTime.Now.ToUnixTimestamp();
                    reply.MsgId = DateTime.Now.Ticks;
                    return reply;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取 request 的处理程序
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static object GetHandler(BaseRequest msg) {
            var attr = msg.GetType().GetCustomAttribute<RequestTypeAttribute>();
            if (attr != null) {
                var type = HandlerTypes.Get(GetKey(attr.MessageType, attr.EventType), null);
                if (type != null) {
                    if (GetService == null)
                        return Activator.CreateInstance(type);
                    else
                        return GetService.Invoke(type);
                }
            }

            return null;
        }
    }
}
