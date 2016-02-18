using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.QQ {

    [Export(typeof(IMessageClient))]
    public class QQClient : BaseMessageClient, IMessageClient {

        public event EventHandler<ProcessedArgs> OnProcessed;

        public override void Init() {
            throw new NotImplementedException();
        }

        public Task Send(BaseMessage msg) {
            throw new NotImplementedException();
        }


        public Type AcceptMessageType {
            get {
                return typeof(QQMessage);
            }
        }
    }
}
