using XXY.WxApi.Attributes;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {

    /// <summary>
    /// 文本内容的处理程序
    /// </summary>
    [RequestType(RequestTypes.Text)]
    public abstract class BaseTextHandler : RequestHandler<TextRequest> {

    }
}
