
namespace XXY.WxApi.Methods {
    /// <summary>
    /// 删除菜单
    /// </summary>
    public class MenuDelete : MethodBase<object> {
        public override string MethodName {
            get {
                return "menu/delete";
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
