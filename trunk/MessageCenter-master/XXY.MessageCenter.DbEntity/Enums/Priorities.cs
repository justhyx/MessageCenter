using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity.Enums {
    [DataContract]
    public enum Priorities : byte {
        [EnumMember]
        Normal = 0,
        [EnumMember]
        Higher = 1,
        [EnumMember]
        Lower = 2,
        /// <summary>
        /// 立即
        /// </summary>
        [EnumMember]
        Immediately = 3
    }
}
