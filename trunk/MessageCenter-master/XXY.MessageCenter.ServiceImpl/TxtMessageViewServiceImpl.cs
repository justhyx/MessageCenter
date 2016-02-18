using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Attributes;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.IService;

namespace XXY.MessageCenter.ServiceImpl {

    [AutoInjection(typeof(ITxtMessageViewService))]
    public class TxtMessageViewServiceImpl : ITxtMessageViewService {


        [Dependency]
        public Lazy<IMessageViewer> Biz {
            get;
            set;
        }

        public async Task<int> GetUnReadCount(decimal receiverID) {
            return await this.Biz.Value.GetUnReadTxtMsgCount(receiverID);
        }


        public async Task<TxtMessage> GetMessage(int msgID, decimal receiverID) {
            return await this.Biz.Value.GetTxtMsg(msgID, receiverID);
        }


        public async Task<bool> SetReaded(int msgID) {
            return await this.Biz.Value.SetTxtMsgReaded(msgID);
        }
    }
}
