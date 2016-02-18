using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.IService;
using XXY.Common.Attributes;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.ServiceImpl {

    [AutoInjection(typeof(ITemplateService))]
    public class TemplateServiceImpl : ITemplateService {

        [Dependency]
        public Lazy<ITemplate> TemplateBiz {
            get;
            set;
        }

        public async Task<Template> GetByCode(string code, string appCode, MsgTypes msgType, Langs? lang = null) {
            var template = await this.TemplateBiz.Value.GetByCode(code, appCode, msgType, lang);
            return template;
        }

        public async Task<IEnumerable<Template>> GetTemplates(string code, string appCode, MsgTypes? msgType = null, Langs? lang = null) {
            var cond = new TemplateSearchCondition() {
                AllowPage = false,
                AppCode = appCode,
                Code = code,
                Lang = lang,
                MsgType = msgType
            };

            return await this.TemplateBiz.Value.Search(cond);
        }
    }
}
