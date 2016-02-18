using System.Collections.Generic;
using XXY.WxApi.Attributes;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Messages;

namespace XXY.WxApi.Methods {
    /// <summary>
    /// 主动发送消息
    /// </summary>
    public class MessageSend : MethodBase<BaseResult> {
        public Message Message {
            get;
            set;
        }


        public override string MethodName {
            get {
                return "message/custom/send";
            }
        }

        public override HttpMethods RequestType {
            get {
                return HttpMethods.Post;
            }
        }

        /// <summary>
        /// 接收人的OPENID
        /// </summary>
        public string OpenID {
            get;
            set;
        }

        protected override object PostData {
            get {
                var t = SpecifyValueAttribute.GetSpecifyValue(this.Message.MsgType).ToString();
                var dic = new Dictionary<string, object>(){
                    {"touser", this.OpenID},
                    {"msgtype", t},
                    { t, this.Message }
                };
                return dic;
            }
        }
    }
}
