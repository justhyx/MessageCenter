using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {
    /// <summary>
    /// 语音消息的处理程序
    /// </summary>
    [RequestType(RequestTypes.Event, EventTypes.Unsubscribe)]
    public abstract class BaseVoiceHandler : RequestHandler<VoiceRequest> {

    }
}
