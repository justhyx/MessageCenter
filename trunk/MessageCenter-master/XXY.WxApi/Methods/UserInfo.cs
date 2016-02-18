using XXY.WxApi.Attributes;
using XXY.WxApi.Entities;

namespace XXY.WxApi.Methods {
    /// <summary>
    /// 获取用户信息
    /// </summary>
    public class UserInfo : MethodBase<User> {
        public override string MethodName {
            get {
                return "user/info";
            }
        }

        public override HttpMethods RequestType {
            get {
                return HttpMethods.Get;
            }
        }

        [Param("openid", Required = true)]
        public string OpenID {
            get;
            set;
        }

        [EnumParam("lang")]
        public Langs? Lang {
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
