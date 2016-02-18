using XXY.WxApi.Entities;

namespace XXY.WxApi.Methods {
    /// <summary>
    /// 获取菜单
    /// </summary>
    public class MenuGet : MethodBase<MenuResult> {
        public override string MethodName {
            get {
                return "menu/get";
            }
        }

        public override HttpMethods RequestType {
            get {
                return HttpMethods.Get;
            }
        }

        protected override object PostData {
            get {
                return null;
            }
        }
    }
}
