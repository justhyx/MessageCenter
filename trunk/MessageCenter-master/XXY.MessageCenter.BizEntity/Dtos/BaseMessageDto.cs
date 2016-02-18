using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.BizEntity.Dtos {
    public abstract class BaseMessageDto {

        public string Ctx {
            get;
            set;
        }

        public string Receiver {
            get;
            set;
        }

        public Priorities PRI {
            get;
            set;
        }

    }
}
