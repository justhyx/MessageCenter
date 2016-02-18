using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.Filters;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.Models;
using XXY.MessageCenter.Res;

namespace XXY.MessageCenter.Controllers {

    [CheckPower(false, Order = 10)]
    public class TxtMsgController : BaseController {

        [Dependency]
        public Lazy<IMessageViewer> Biz {
            get;
            set;
        }

        public async Task<ActionResult> Index() {
            var cond = this.GetCondition<TxtMsgSeachCondition>();
            if (cond == null)
                return View();
            else
                return await Index(cond);
        }

        [HttpPost]
        public async Task<ActionResult> Index(TxtMsgSeachCondition condition) {
            var datas = await this.Biz.Value.Search(condition);
            var datas2 = datas.Select(d => (TxtMessage)d);
            return View(PDM.Create(datas2, condition));
        }

        public async Task<ActionResult> Detail(int id) {
            var msg = await this.Biz.Value.GetTxtMsg(id, this.CurrentUser.UserID, true);
            if (msg != null)
                return View(msg);
            else
                return HttpNotFound();
        }

        public async Task<ActionResult> Delete(int id, TxtMsgSeachCondition condition) {
            if (await this.Biz.Value.DeleteTxtMsg(id, this.CurrentUser.UserID)) {
                this.SetMessage(StaticRes.DeleteSuccess);
            }
            this.SetCondition(condition);
            return RedirectToAction("Index");
        }
    }
}