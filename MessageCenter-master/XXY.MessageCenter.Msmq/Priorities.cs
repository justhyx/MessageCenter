using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.Queue {
    public enum Priorities : byte {
        Normal = 1,
        Higher = 2,
        Lower = 4,
        /// <summary>
        /// 立即
        /// </summary>
        Immediately = 8
    }
}
