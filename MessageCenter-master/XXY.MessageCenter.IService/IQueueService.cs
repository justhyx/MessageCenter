using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.BizEntity.Dtos;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.IService {


    [ServiceKnownType(typeof(WeChatDto))]
    [ServiceKnownType(typeof(QQDto))]
    [ServiceKnownType(typeof(SMSDto))]
    [ServiceKnownType(typeof(TxtDto))]
    [ServiceKnownType(typeof(EmailDto))]
    [ServiceContract]
    public interface IQueueService {

        [OperationContract]
        Task<bool> Put(BaseMessageDto msg);

    }
}
