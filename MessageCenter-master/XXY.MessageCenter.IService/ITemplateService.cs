using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.IService {
    [ServiceContract]
    public interface ITemplateService {

        /// <summary>
        /// 获取消息模板
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lang"></param>
        /// <param name="appCode"></param>
        /// <param name="msgType">消息类型</param>
        /// <returns></returns>
        [OperationContract]
        Task<Template> GetByCode(string code, string appCode, MsgTypes msgType, Langs? lang = null);

        [OperationContract]
        Task<IEnumerable<Template>> GetTemplates(string code, string appCode, MsgTypes? msgType = null, Langs? lang = null);
    }
}
