using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {

    [RequestType(RequestTypes.Link)]
    public abstract class BaseLinkHandler : RequestHandler<LinkRequest> {

    }
}
