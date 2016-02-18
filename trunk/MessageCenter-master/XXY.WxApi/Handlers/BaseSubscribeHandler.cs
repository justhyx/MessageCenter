using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {

    /// <summary>
    /// 订阅事件的处理程序
    /// </summary>
    [RequestType(RequestTypes.Event, EventTypes.Subscribe)]
    public abstract class BaseSubscribeHandler : RequestHandler<SubscribeRequest> {

    }
}
