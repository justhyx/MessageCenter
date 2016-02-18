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
    /// 短信
    /// </summary>
    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    public class SMSMessage : BaseMessage {

        public SMSMessage()
            : base(Enums.MsgTypes.SMS) {
        }


        [DataMember]
        [Required, StringLength(20)]
        new public string Receiver {
            get;
            set;
        }


        [DataMember]
        [Required, StringLength(200)]
        new public string Ctx {
            get;
            set;
        }
    }
}
