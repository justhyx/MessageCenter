using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {

    /// <summary>
    /// 取消关注的事件处理程序
    /// </summary>
    [RequestType(RequestTypes.Event, EventTypes.Unsubscribe)]
    public abstract class BaseUnsubscribeHandler : RequestHandler<UnsubscribeRequest> {
        
    }
}
