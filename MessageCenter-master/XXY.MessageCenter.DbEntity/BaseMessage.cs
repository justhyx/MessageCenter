using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.DbEntity {

    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    public abstract class BaseMessage : BaseEntity {

        [NotMapped]
        public MsgTypes MsgType {
            get;
            private set;
        }

        [NotMapped]
        public bool AllowHtml {
            get;
            set;
        }



        public BaseMessage(MsgTypes type, bool allowHtml = false) {
            this.MsgType = type;
            this.AllowHtml = allowHtml;
            this.PRI = Priorities.Normal;
            this.Status = MsgStatus.New;
        }


        [DataMember]
        public Priorities PRI {
            get;
            set;
        }

        [DataMember]
        [Required]
        public string Ctx {
            get;
            set;
        }

        [DataMember]
        [Required]
        public string Receiver {
            get;
            set;
        }

        [DataMember]
        public MsgStatus Status {
            get;
            set;
        }

        /// <summary>
        /// 仅用于关联查询,临时数据赋值
        /// </summary>
        [NotMapped]
        public string ErrorInfo {
            get;
            set;
        }
    }
}
