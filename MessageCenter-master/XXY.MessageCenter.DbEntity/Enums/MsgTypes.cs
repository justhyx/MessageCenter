using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity.Enums {

    [DataContract]
    public enum MsgTypes : byte {
        [EnumMember]
        Email = 0,
        [EnumMember]
        SMS = 1,
        [EnumMember]
        WeChat = 2,
        [EnumMember]
        QQ = 3,
        [EnumMember]
        Txt = 4
    }
}
