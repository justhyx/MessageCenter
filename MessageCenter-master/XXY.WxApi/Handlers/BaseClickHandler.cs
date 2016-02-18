using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {

    /// <summary>
    /// Click 菜单的处理程序
    /// </summary>
    [RequestType(RequestTypes.Event, EventTypes.Click)]
    public abstract class BaseClickHandler : RequestHandler<ClickRequest> {
        
    }
}
