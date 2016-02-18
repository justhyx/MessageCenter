using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity.Enums {

    [DataContract]
    public enum MsgStatus : byte {
        [EnumMember]
        New = 0,
        [EnumMember]
        Processing = 1,
        [EnumMember]
        Processed = 2,
        [EnumMember]
        Failed = 3
    }
}
