using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.DbEntity {
    public class FailedMessage : BaseEntity {

        public int MsgID {
            get;
            set;
        }

        public MsgTypes MsgType {
            get;
            set;
        }

        [StringLength(1000)]
        public string Log {
            get;
            set;
        }
    }
}
