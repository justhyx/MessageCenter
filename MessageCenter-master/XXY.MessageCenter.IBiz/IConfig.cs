using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.IBiz {
    public interface IConfig {

        string MessageMSMQPath {
            get;
        }

        string ProcessedMessageMSMQPath {
            get;
        }
    }
}
