using XXY.WxApi.Entities;

namespace XXY.WxApi.Methods {
    /// <summary>
    /// 创建菜单
    /// </summary>
    public class MenuCreate : MethodBase<object> {

        public override string MethodName {
            get {
                return "menu/create";
            }
        }

        public override HttpMethods RequestType {
            get {
                return HttpMethods.Post;
            }
        }

        public Menu Menu {
            get;
            set;
        }

        protected override object PostData {
            get {
                return this.Menu;
            }
        }
    }
}
