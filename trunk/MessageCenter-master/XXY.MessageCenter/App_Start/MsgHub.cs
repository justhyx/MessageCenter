using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using XXY.Common.Extends;
using Microsoft.Practices.Unity;
using XXY.MessageCenter.IBiz;
using Microsoft.Practices.ServiceLocation;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using XXY.MessageCenter.DbEntity;
using XXY.Common;

namespace XXY.MessageCenter {

    [HubName("MsgHub")]
    public class MsgHub : Hub {

        private IMessageViewer Biz = null;

        public MsgHub() {
            this.Biz = ServiceLocator.Current.GetInstance<IMessageViewer>();
        }

        public async Task<int> UnReadCount() {
            var uid = this.GetUserID();
            if (uid.HasValue) {
                return await this.Biz.GetUnReadTxtMsgCount(uid.Value);
            }
            return 0;
        }

        public async Task<IEnumerable<TxtMessage>> ListMsgs(Pager pager = null, bool onlyUnread = true) {
            var uid = this.GetUserID();
            if (uid.HasValue)
                return await this.Biz.GetTxtMsg(uid.Value, pager, onlyUnread);
            else
                return Enumerable.Empty<TxtMessage>();
        }

        private decimal? GetUserID() {
            var provider = (IUserIdProvider)GlobalHost.DependencyResolver.GetService(typeof(IUserIdProvider));
            var uid = provider.GetUserId(Context.Request).ToDecimalOrNull();
            return uid;
        }
    }
}