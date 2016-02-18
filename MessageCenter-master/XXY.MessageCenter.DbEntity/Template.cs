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
    public class Template : BaseEntity {


        [DataMember]
        [Required]
        [StringLength(30)]
        public string Code {
            get;
            set;
        }

        [DataMember]
        public MsgTypes MsgType {
            get;
            set;
        }


        [DataMember]
        public Langs Lang {
            get;
            set;
        }

        [DataMember]
        [Required]
        [StringLength(30)]
        public string AppCode {
            get;
            set;
        }

        [DataMember]
        public bool IsDefault {
            get;
            set;
        }

        [DataMember]
        [StringLength(200), Required]
        public string Subject {
            get;
            set;
        }


        [DataMember]
        public string Ctx {
            get;
            set;
        }
    }
}
