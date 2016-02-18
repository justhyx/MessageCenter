using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.Common.Attributes;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.BizEntity.Conditions {
    public class TemplateSearchCondition : BaseQuery<Template> {

        [MapTo("Code", IgnoreCase = true, Opt = MapToOpts.Include)]
        public string Code {
            get;
            set;
        }

        [MapTo("AppCode", IgnoreCase = true)]
        public string AppCode {
            get;
            set;
        }

        [MapTo("Lang")]
        public Langs? Lang {
            get;
            set;
        }

        [MapTo("MsgTpye")]
        public MsgTypes? MsgType {
            get;
            set;
        }


        [MapTo("Subject", Opt = MapToOpts.Include, IgnoreCase = true)]
        public string Subject {
            get;
            set;
        }
    }
}
