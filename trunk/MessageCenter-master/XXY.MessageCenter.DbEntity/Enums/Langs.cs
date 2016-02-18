using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity.Enums {

    [DataContract]
    public enum Langs : byte {
        [EnumMember]
        ZhCn = 0,
        [EnumMember]
        EnUs = 1,
        [EnumMember]
        ZhTw = 2
    }
}
