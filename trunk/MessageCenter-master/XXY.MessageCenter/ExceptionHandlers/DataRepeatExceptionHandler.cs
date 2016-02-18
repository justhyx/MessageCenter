using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXY.Common.Exceptions;

namespace XXY.MessageCenter.ExceptionHandlers {
    public class DataRepeatExceptionHandler : BaseExceptionHandler {

        public override bool CanDeal(Type exType) {
            return exType.IsGenericType
                && exType
                    .GetGenericTypeDefinition()
                    .Equals(typeof(DataRepeatException<>));
        }

        protected override int StatusCode {
            get {
                return 200;
            }
        }

        protected override string ViewName {
            get {
                return "DataRepeat";
            }
        }

        //public override void DealException(ExceptionContext filterContext, Common.ILog log) {
        //    if (!filterContext.ExceptionHandled) {
        //        dynamic ex = filterContext.Exception;

        //        var controller = (Controller)filterContext.Controller;
        //        controller.ModelState.AddModelError("数据重复", "");

        //        //filterContext.Result = 
        //        filterContext.ExceptionHandled = true;
        //        //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        //    }
        //}
    }
}