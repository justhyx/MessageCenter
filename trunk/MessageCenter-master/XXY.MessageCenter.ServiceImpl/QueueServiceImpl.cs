using AutoMapper;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Attributes;
using XXY.MessageCenter.BizEntity.Dtos;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.IService;

namespace XXY.MessageCenter.ServiceImpl {

    [AutoInjection(typeof(IQueueService))]
    public class QueueServiceImpl : IQueueService {

        [Dependency]
        public Lazy<IQueue> QueueBiz {
            get;
            set;
        }


        public async Task<bool> Put(BaseMessageDto msg) {
            var data = Mapper.Map<BaseMessage>(msg);
            return await this.QueueBiz.Value.Put(data);
        }
    }
}
