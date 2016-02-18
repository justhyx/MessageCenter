using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXY.Common.Exceptions;

namespace XXY.MessageCenter.Filters {
    public class NoPermissionExceptionHandlerAttribute : FilterAttribute, IExceptionFilter {

        public void OnException(ExceptionContext filterContext) {
            if (filterContext.Exception is NoPermissionException) {
                ViewResult result = new ViewResult {
                    ViewName = "Denied",
                    TempData = filterContext.Controller.TempData
                };
                filterContext.Result = result;
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 200;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}