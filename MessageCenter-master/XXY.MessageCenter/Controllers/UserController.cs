using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using XXY.Common;
using XXY.Common.Extends;
using XXY.Configuration;
using XXY.UC.BizEntity;
using XXY.UC.IService;

namespace XXY.MessageCenter.Controllers
{
    public class UserController : BaseController {
        // GET: User
        public ActionResult Index() {
            return View();
        }

        [ChildActionOnly]
        public ActionResult TopMenu() {
            return this.Menu(true);
        }

        [ChildActionOnly]
        public ActionResult Menu(bool isTop = false) {
            var view = isTop ? "TopMenu" : "menu";

            if (this.CurrentUser != null) {
                var menus = this.LoadMenu(this.CurrentUser)
                    .Where(m => m.Parent == null && m.SubMenus.Count > 0).ToList();

                return View(view, menus);
            } else
                return View(view);
        }

        private IEnumerable<MenuItem> DealMenu(IEnumerable<MenuItem> items) {
            foreach (var m in items) {
                var subs = items.Where(i => i.ParentID == m.ModuleID).ToList();
                subs.ForEach(s => s.Parent = m);
                m.SubMenus.AddRange(subs);
            }
            return items;
        }

        private IEnumerable<MenuItem> LoadMenu(User user) {
            var appCode = ConfigurationHelper.GetSection<CurrentSystem>().SystemConfig.AppCode;

            var menus = SessionHelper.Get<Dictionary<string, IEnumerable<MenuItem>>>(SessionKeys.Menus.ToString());
            if (menus == null || menus.Get(appCode, null) == null) {
                var sysConf = ConfigurationHelper.GetSection<SystemsConfig>().Systems.Get(appCode);

                IEnumerable<MenuItem> appMenus = new List<MenuItem>();
                using (var factory = new ChannelFactory<IUserService>("*")) {
                    var svc = factory.CreateChannel();
                    appMenus = svc.GetMenus(user.UserID, sysConf.AppID, (UC.DbEntity.PlatformType)sysConf.Platform);
                }
                appMenus = this.DealMenu(appMenus);
                if (menus == null)
                    menus = new Dictionary<string, IEnumerable<MenuItem>>();

                menus.Set(appCode, appMenus);
                SessionKeys.Menus.Set(menus);

                Response.Clear();
                Response.Redirect(Request.RawUrl);
                Response.End();

                return appMenus;
            } else
                return menus.Get(appCode);
        }
    }
}