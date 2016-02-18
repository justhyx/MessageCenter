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
    /// QQ
    /// </summary>

    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    public class QQMessage : BaseMessage {

        public QQMessage()
            : base(Enums.MsgTypes.QQ) {
        }


        [DataMember]
        [Required, StringLength(20)]
        new public string Receiver {
            get;
            set;
        }

        [DataMember]
        [Required, StringLength(1000)]
        new public string Ctx {
            get;
            set;
        }

    }
}
