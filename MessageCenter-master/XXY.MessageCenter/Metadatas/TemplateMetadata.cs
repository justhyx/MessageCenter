using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using XXY.Common.Attributes;
using XXY.MessageCenter.DbEntity;


namespace XXY.Mail.Metadatas {
    
    [AnnoationFor(typeof(Template))]
    public class TemplateMetadata {

        [AllowHtml]
        public object Ctx {
            get;
            set;
        }

    }
}
