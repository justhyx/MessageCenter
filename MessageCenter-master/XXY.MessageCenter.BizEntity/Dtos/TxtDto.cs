using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.BizEntity.Dtos {
    public class TxtDto : BaseMessageDto {

        public string Subject {
            get;
            set;
        }

        public string Sender {
            get;
            set;
        }

        public decimal SenderID {
            get;
            set;
        }

        public decimal ReceiverID {
            get;
            set;
        }
    }
}
