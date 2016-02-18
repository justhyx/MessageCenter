using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXY.Common;
using XXY.Common.Extends;
using XXY.Configuration;
using XXY.UC.BizEntity;


namespace XXY.MessageCenter.Filters {

    /// <summary>
    /// 登陆过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class NeedLoginAttribute : ActionFilterAttribute {

        public bool Need;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="need">如果为false, 即不需要登陆</param>
        public NeedLoginAttribute(bool need = true) {
            this.Need = need;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            if (filterContext.Controller.ControllerContext.IsChildAction)
                return;

            if (this.Need) {
                var user = SessionHelper.Get<User>(SessionKeys.User.ToString());
                if (user == null) {
                    var url = ConfigurationHelper.GetSection<PathConfig>().Paths.Get("Login").Path;
                    filterContext.Result = new RedirectResult(url.SetUrlKeyValue("url", filterContext.HttpContext.Request.Url.AbsoluteUri));
                }
            }

            base.OnActionExecuting(filterContext);
        }

    }
}