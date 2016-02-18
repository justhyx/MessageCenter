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
    public class TemplateController : BaseController {

        [Dependency]
        public Lazy<ITemplate> Biz {
            get;
            set;
        }

        public async Task<ActionResult> Index() {
            var cond = this.GetCondition<TemplateSearchCondition>();
            if (cond != null)
                return await Index(cond);
            else
                return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(TemplateSearchCondition condition) {
            return View(PDM.Create(await this.Biz.Value.Search(condition), condition));
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(
            [Bind(Include = "AppCode,Code,Lang,MsgType,Subject,Ctx,IsDefault")]
            Template template
            ) {
            await this.Biz.Value.Create(template);
            if (!this.Biz.Value.HasError) {
                this.SetMessage(StaticRes.SaveSuccess);
                return RedirectToAction("Index");
            }

            this.ParseBizError(this.Biz.Value);
            this.SetRedirectModelState();
            return RedirectToAction("Create");
        }

        public async Task<ActionResult> Detail(int id) {
            var data = await this.Biz.Value.GetBySeq(id);
            if (data != null) {
                ViewBag.IsEdit = false;
                return View("Create", data);
            } else {
                return HttpNotFound();
            }
        }

        public async Task<ActionResult> Delete(int id) {
            if (await this.Biz.Value.Delete(id)) {
                this.SetMessage(StaticRes.DeleteSuccess);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id) {
            var data = await this.Biz.Value.GetBySeq(id);
            if (data != null) {
                return View("Create", data);
            } else {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id,
            [Bind(Include = "AppCode,Code,Lang,MsgType,Subject,Ctx,IsDefault")]
            Template template) {
            await this.Biz.Value.Edit(id, template);
            if (!this.Biz.Value.HasError) {
                this.SetMessage(StaticRes.SaveSuccess);
                return RedirectToAction("Index");
            }
            this.ParseBizError(this.Biz.Value);
            this.SetRedirectModelState();
            return RedirectToAction("Edit", new {
                id = id
            });
        }
    }
}