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

namespace XXY.MessageCenter.DbEntity {

    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    public abstract class BaseEntity {

        [DataMember]
        [Key, Column("ID", Order = 0)]
        public int ID {
            get;
            set;
        }


        [DataMember]
        public DateTime CreateOn {
            get;
            set;
        }


        [DataMember]
        [StringLength(20), Required]
        public string CreateByUserName {
            get;
            set;
        }

        [DataMember]
        public decimal CreateByUserID {
            get;
            set;
        }


        [DataMember]
        public DateTime? ModifyOn {
            get;
            set;
        }


        [DataMember]
        [StringLength(20)]
        public string ModifyByUserName {
            get;
            set;
        }


        [DataMember]
        public decimal? ModifyByUserID {
            get;
            set;
        }

        [DataMember]
        public bool IsDeleted {
            get;
            set;
        }
    }
}
