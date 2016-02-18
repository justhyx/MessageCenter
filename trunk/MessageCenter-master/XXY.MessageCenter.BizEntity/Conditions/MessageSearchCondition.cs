using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.Common.Attributes;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.BizEntity.Conditions {
    public class MessageSearchCondition : BaseQuery<BaseMessage> {

        //Message 表中并没有 MsgTypes 这个字段
        //[MapTo("MsgType")]
        public MsgTypes MsgType {
            get;
            set;
        }

        [MapTo("Receiver", Opt = MapToOpts.Include, IgnoreCase = true)]
        public string Receiver {
            get;
            set;
        }

        [MapTo("Status")]
        public MsgStatus? Status {
            get;
            set;
        }
    }
}
