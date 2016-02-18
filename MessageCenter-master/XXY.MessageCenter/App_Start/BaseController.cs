using log4net;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using XXY.Common;
using XXY.Common.Extends;
using XXY.Configuration;
using XXY.MessageCenter.Filters;
using XXY.MessageCenter.IBiz;
using XXY.UC.BizEntity;

namespace XXY.MessageCenter {
    /// <summary>
    /// <remarks>
    /// ActionFilter 的前10个顺序预留在 BaseController 内使用,其它的Controller Order 从10开始
    /// </remarks>
    /// </summary>
    [NeedLogin(Order = 0), ActionParameterCheck(Order = 2), CheckPower(Order = 3)]
    public class BaseController : Controller {

        [Dependency]
        public XXY.Common.ILog Logger {
            get;
            set;
        }

        /// <summary>
        /// 当前系统的代码
        /// </summary>
        private static string AppCode;

        static BaseController() {
            AppCode = ConfigurationHelper.GetSection<CurrentSystem>().SystemConfig.AppCode;
        }


        /// <summary>
        /// 当前登陆的用户
        /// </summary>
        protected User CurrentUser {
            get;
            private set;
        }

        /// <summary>
        /// 当前登陆用户在当前系统中的可用菜单项
        /// </summary>
        protected IEnumerable<MenuItem> CurrentMenu {
            get;
            private set;
        }

        /// <summary>
        /// 是否平台管理员
        /// </summary>
        protected bool IsAdmin {
            get {
                return (bool)this.RouteData.Values["isAdmin"];
            }
        }

        public BaseController() {

            this.CurrentUser = SessionHelper.Get<User>(SessionKeys.User.ToString());
            var menus = SessionHelper.Get<Dictionary<string, IEnumerable<MenuItem>>>(SessionKeys.Menus.ToString());
            if (menus != null)
                this.CurrentMenu = menus.Get(AppCode, null);

            //当前登陆用户的母公司ID, 用于JS
            this.ViewBag.RootCompanyID = this.CurrentUser != null ? this.CurrentUser.RootCompanyID : 0;
        }


        protected virtual void SetMessage(string msg) {
            this.TempData["Message"] = msg;
        }

        /// <summary>
        /// 输出alert提示且关闭弹出层窗口
        /// </summary>
        /// <param name="msg"></param>
        protected virtual void SetMessage(string msg, bool callBack = false) {
            this.TempData["CallBack"] = callBack;
            this.TempData["Message"] = msg;
        }


        /// <summary>
        /// 将 Biz 中的错误转换为 ModelState 中的错误
        /// </summary>
        /// <param name="biz"></param>
        protected virtual void ParseBizError(IBaseBiz biz) {
            if (biz.HasError) {
                foreach (var err in biz.Errors) {
                    this.ModelState.AddModelError(err.Key, string.IsNullOrWhiteSpace(err.Value) ? err.Key : err.Value);
                }

                var f = biz.Errors.First();
                this.SetMessage(string.IsNullOrWhiteSpace(f.Value) ? f.Key : f.Value);
            }
        }



        /// <summary>
        /// 附加 BreadCrumb
        /// </summary>
        /// <param name="strs"></param>
        protected void SetBreadCrumb(params string[] strs) {
            var action = this.RouteData.Values["action"];
            var controller = this.RouteData.Values["controller"];

            string key = string.Format("BreadCrumb_{0}_{1}", controller, action);
            List<string> crumbs = (List<string>)this.TempData[key];
            if (crumbs == null)
                crumbs = new List<string>();

            crumbs.AddRange(strs);
            this.TempData.Set(key, crumbs);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext) {
            if (TempData["ModelState"] != null && !ModelState.Equals(TempData["ModelState"]))
                ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);

            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 在跳转之前, 将当前的 ModelState 合并, 使跳转后, ModelState 仍然有效
        /// </summary>
        protected virtual void SetRedirectModelState() {
            this.TempData.Add("ModelState", this.ModelState);
        }

        protected void SetCondition<T>(T condition) where T : BaseQuery {
            if (condition != null)
                this.TempData.Set("condition", condition);
        }

        protected T GetCondition<T>() where T : BaseQuery {
            object cond;
            if (this.TempData.TryGetValue("condition", out cond)) {
                return (T)cond;
            }
            return null;
        }
    }
}
