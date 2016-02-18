using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;
using XXY.Common.Extends;
using Microsoft.AspNet.SignalR.Hubs;

namespace XXY.MessageCenter.TxtMsgHub {

    /// <summary>
    /// 即时消息 Hub
    /// </summary>
    [HubName("IMHub")]
    public class IMHub : Hub {

    }

}
