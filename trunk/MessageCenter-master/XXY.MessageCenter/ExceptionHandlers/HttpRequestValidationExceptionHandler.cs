using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace XXY.MessageCenter.ExceptionHandlers {
    /// <summary>
    /// 危险字符串处理器
    /// </summary>
    public class HttpRequestValidationExceptionHandler : BaseExceptionHandler {
        protected override int StatusCode {
            get {
                return 200;
            }
        }

        protected override string ViewName {
            get {
                return "UnSafeString";
            }
        }

        public override bool CanDeal(Type exType) {
            return exType.Equals(typeof(HttpRequestValidationException));
        }

        protected override void BeforeDeal(ExceptionContext filterContext) {
            if (filterContext.IsChildAction) {
                ViewContext par = filterContext.ParentActionViewContext;
                while (null != par) {
                    var wtr = (StringWriter)par.Writer;
                    wtr.GetStringBuilder().Clear();
                    par = par.ParentActionViewContext;
                }
            }
        }

        protected override HandleErrorInfo GetHandleErrorInfo(Exception ex, string controllerName, string actionName) {
            return new HandleErrorInfo(new Exception("不安全的字符串"), controllerName, actionName);
        }
    }
}