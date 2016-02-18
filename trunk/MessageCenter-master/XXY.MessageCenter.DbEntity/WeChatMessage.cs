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
    /// 微信消息
    /// </summary>
    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    public class WeChatMessage : BaseMessage {

        public WeChatMessage()
            : base(Enums.MsgTypes.WeChat) {

        }


        [DataMember]
        [Required, StringLength(100)]
        new public string Receiver {
            get {
                return base.Receiver;
            }
            set {
                base.Receiver = value;
            }
        }

    }
}
