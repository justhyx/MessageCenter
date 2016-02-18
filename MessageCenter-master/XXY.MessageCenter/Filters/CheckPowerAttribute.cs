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
    /// 权限检查过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CheckPowerAttribute : ActionFilterAttribute {


        private static string AppCode;

        static CheckPowerAttribute() {
            AppCode = ConfigurationHelper.GetSection<CurrentSystem>().SystemConfig.AppCode;
        }

        private bool Check;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="check">是否检查, 如果为 false 不检查权限</param>
        public CheckPowerAttribute(bool check = true) {
            this.Check = check;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            //ChildAction 不需要检查
            if (filterContext.Controller.ControllerContext.IsChildAction) {
                return;
            }

            //Controller 上的 NeedLogin 
            var attrs = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(true).OfType<NeedLoginAttribute>().ToList();

            ////Action 上的 NeedLogin 
            attrs.AddRange(filterContext.ActionDescriptor.GetCustomAttributes(true).OfType<NeedLoginAttribute>());

            //按照Order 降序的第一个, XXXExecuting 是按正序堆叠的
            var needLogin = attrs.OrderByDescending(a => a.Order).FirstOrDefault();

            //如果不需要登陆, 就不用检查权限
            if (needLogin != null && !needLogin.Need)
                this.Check = false;

            var controller = (string)filterContext.Controller.ControllerContext.RouteData.Values["controller"];
            var action = (string)filterContext.Controller.ControllerContext.RouteData.Values["action"];
            var area = (string)filterContext.Controller.ControllerContext.RouteData.DataTokens["area"];

            MenuItem menu = null;

            if (this.Check) {
                menu = this.GetMenu(controller, action, area);
                if (menu != null) {
                    //OK, 有权限
                    filterContext.Controller.TempData["CurrentMenu"] = menu;
                } else {
                    //没有权限
                    filterContext.Controller.TempData["CurrentMenu"] = null;
                    filterContext.Result = new ViewResult() {
                        ViewName = "Denied",
                        TempData = filterContext.Controller.TempData
                    };
                    filterContext.HttpContext.Response.Clear();
                }
            } else {
                //不检查权限，设置 BreadCrumb 为
                menu = this.GetMenu(controller, "Index", area);
                filterContext.Controller.TempData["CurrentMenu"] = menu;
            }
        }


        private MenuItem GetMenu(string controller, string action, string area) {
            var menuDic = SessionHelper.Get<Dictionary<string, IEnumerable<MenuItem>>>(SessionKeys.Menus.ToString());
            if (menuDic != null) {
                var menus = menuDic.Get(AppCode, null);

                if (menus != null && menus.Count() > 0) {
                    var url = string.Format("/{0}/{1}/{2}", area, controller, action).Replace("//", "/");

                    var menu = menus.FirstOrDefault(m =>
                        url.Equals(m.Url, StringComparison.OrdinalIgnoreCase)
                        );

                    return menu;
                }
            }

            return null;
        }

    }
}