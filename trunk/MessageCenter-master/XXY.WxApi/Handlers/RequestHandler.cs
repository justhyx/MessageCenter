using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi.Handlers {

    /// <summary>
    /// request 处理程序
    /// </summary>
    public abstract class RequestHandler {
        public abstract Reply Handle(string tag, BaseRequest msg);
    }

    /// <summary>
    /// request 处理程序
    /// </summary>
    public abstract class RequestHandler<TRequest> : RequestHandler where TRequest : BaseRequest {
        public abstract Reply Handle(string tag, TRequest request);


        public override Reply Handle(string tag, BaseRequest request) {
            return this.Handle(tag, (TRequest)request);
        }
    }
}
