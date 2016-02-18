using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.DbEntity {
    public class ProcessedMsg {

        public MsgTypes MsgType {
            get;
            set;
        }

        public int MsgID {
            get;
            set;
        }

        public bool IsSuccessed {
            get;
            set;
        }

        public string Error {
            get;
            set;
        }

    }
}
