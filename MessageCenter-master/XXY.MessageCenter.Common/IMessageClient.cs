using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.Common {

    public interface IMessageClient {

        Task Send(BaseMessage msg);

        Type AcceptMessageType {
            get;
        }

        event EventHandler<ProcessedArgs> OnProcessed;
    }

    //public interface IMessageClientMetadata {
    //    MsgTypes MsgType {
    //        get;
    //    }
    //}
}
