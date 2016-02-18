using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {

    [RequestType(RequestTypes.Event, EventTypes.View)]
    public abstract class BaseViewHandler : RequestHandler<ViewRequest> {
    }
}
