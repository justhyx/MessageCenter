using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity {

    /// <summary>
    /// 文本消息
    /// </summary>
    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    public class TxtMessage : BaseMessage {


        public TxtMessage()
            : base(Enums.MsgTypes.Txt, true) {
        }


        [DataMember]
        [Required, StringLength(100)]
        public string Subject {
            get;
            set;
        }

        [DataMember]
        public bool Readed {
            get;
            set;
        }

        [DataMember]
        [Required, StringLength(20)]
        public string Sender {
            get;
            set;
        }


        [DataMember]
        public decimal SenderID {
            get;
            set;
        }

        [DataMember]
        public decimal ReceiverID {
            get;
            set;
        }

        [DataMember]
        [Required, StringLength(20)]
        new public string Receiver {
            get {
                return base.Receiver;
            }
            set {
                base.Receiver = value;
            }
        }

        [DataMember]
        [Required, StringLength(1000)]
        new public string Ctx {
            get {
                return base.Ctx;
            }
            set {
                base.Ctx = value;
            }
        }
    }
}
