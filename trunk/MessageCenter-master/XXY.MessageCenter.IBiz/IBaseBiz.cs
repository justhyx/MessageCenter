using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.IBiz {
    public interface IBaseBiz {
        bool HasError {
            get;
        }

        Dictionary<string, string> Errors {
            get;
            set;
        }
    }

    public interface IBaseBiz<T> : IBaseBiz {
        Task<T> GetBySeq(decimal id);

        Task<bool> Delete(decimal id);
    }
}
