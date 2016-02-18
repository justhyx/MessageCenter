using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXY.Common;

namespace XXY.MessageCenter.ExceptionHandlers {

    /// <summary>
    /// 异常处理器
    /// </summary>
    public class BaseExceptionHandler {


        /// <summary>
        /// 默认异常处理器
        /// </summary>
        public static BaseExceptionHandler DefaultHandler = new BaseExceptionHandler();

        /// <summary>
        /// 处理器返回的 Http 状态
        /// </summary>
        protected virtual int StatusCode {
            get {
                return 500;
            }
        }

        /// <summary>
        /// 处理器的呈现视图
        /// </summary>
        protected virtual string ViewName {
            get {
                return "Exception";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual bool ExceptionHandled {
            get {
                return true;
            }
        }

        /// <summary>
        /// 处理器是否能处理<paramref name="exType"/> 所指类型的异常
        /// </summary>
        /// <param name="exType"></param>
        /// <returns></returns>
        public virtual bool CanDeal(Type exType) {
            return false;
        }

        protected virtual void BeforeDeal(ExceptionContext filterContext) {

        }

        protected virtual HandleErrorInfo GetHandleErrorInfo(Exception ex, string controllerName, string actionName) {
            return new HandleErrorInfo(ex, controllerName, actionName);
        }

        public virtual void DealException(ExceptionContext filterContext, ILog log) {
            if (!filterContext.ExceptionHandled) {
                this.BeforeDeal(filterContext);

                HandleErrorInfo model = null;
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];

                var ex = filterContext.Exception.GetBaseException();
                log.Log(ex);

                model = this.GetHandleErrorInfo(ex, controllerName, actionName);

                ViewResult result = new ViewResult {
                    ViewName = this.ViewName,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
                filterContext.Result = result;
                filterContext.ExceptionHandled = this.ExceptionHandled;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = this.StatusCode;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}