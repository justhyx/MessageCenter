using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXY.Common;
using XXY.Common.MVC;
using XXY.MessageCenter.Res;

namespace XXY.MessageCenter {
    public class Lang : ILang, IEnumDescriptionProvider {
        public ModelMetadataProvider GetMetadataProvider() {
            return new ResDataAnnotationsModelMetadataProvider(EntityRes.ResourceManager);
        }


        public string GetByKey(string key) {
            return EntityRes.ResourceManager.GetString(key);
        }

        public string GetDescription(string key) {
            return EnumRes.ResourceManager.GetString(key);
        }
    }
}