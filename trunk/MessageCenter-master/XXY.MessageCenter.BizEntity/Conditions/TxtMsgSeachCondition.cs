using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.Common.Attributes;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.BizEntity.Conditions {
    public class TxtMsgSeachCondition : MessageSearchCondition {

        public TxtMsgSeachCondition() {
            this.MsgType = DbEntity.Enums.MsgTypes.Txt;
        }

        [MapTo("ReceiverID")]
        public double ReceiverID {
            get;
            set;
        }

        [MapTo("Subject", Opt = MapToOpts.Include, IgnoreCase = true)]
        public string Subject {
            get;
            set;
        }


        [MapTo("Readed")]
        public bool? OnlyReaded {
            get;
            set;
        }

    }
}
