using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.BizEntity;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.IBiz {

    /// <summary>
    /// 模板
    /// </summary>
    public interface ITemplate : IBaseBiz<Template> {

        Task Create(Template entry);

        Task Edit(int id, Template entry);

        Task<Template> GetByCode(string code, string appCode, MsgTypes msgType, Langs? lang);

        Task<IEnumerable<Template>> Search(TemplateSearchCondition cond);
    }
}
