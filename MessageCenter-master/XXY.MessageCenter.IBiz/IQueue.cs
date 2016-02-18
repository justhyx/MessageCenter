using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.IBiz {
    public interface IQueue : IBaseBiz {

        Task<bool> Put<T>(T msg) where T : BaseMessage;

        Task Update(IEnumerable<ProcessedMsg> msgs);
    }
}
