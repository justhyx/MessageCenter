using XXY.WxApi.Attributes;
using XXY.WxApi.Entities;

namespace XXY.WxApi.Methods {
    /// <summary>
    /// 
    /// </summary>
    public class GetToken : MethodBase<AccessToken> {
        public override string MethodName {
            get {
                return "token";
            }
        }

        public override HttpMethods RequestType {
            get {
                return HttpMethods.Get;
            }
        }

        [Param("grant_type")]
        public string GrantType {
            get {
                return "client_credential";
            }
        }

        [Param("appid")]
        public string AppID {
            get;
            set;
        }

        [Param("secret")]
        public string Secret {
            get;
            set;
        }

        protected override object PostData {
            get {
                return null;
            }
        }
    }
}
