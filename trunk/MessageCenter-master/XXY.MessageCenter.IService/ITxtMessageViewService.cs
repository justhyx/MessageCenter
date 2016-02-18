using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.IService {

    [ServiceContract]
    public interface ITxtMessageViewService {

        [OperationContract]
        Task<int> GetUnReadCount(decimal receiverID);

        [OperationContract]
        Task<TxtMessage> GetMessage(int msgID, decimal receiverID);

        [OperationContract]
        Task<bool> SetReaded(int msgID);
    }
}
