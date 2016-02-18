using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using XXY.Common;
using XXY.Common.MVC;

namespace XXY.MessageCenter {
    public class LangConfig {

        /// <summary>
        /// 设定多语言的 Provider
        /// </summary>
        public static void Config() {
            ILang lang = DependencyResolver.Current.GetService<ILang>();
            ModelMetadataProviders.Current = lang.GetMetadataProvider();

            XXY.Common.Extends.EnumHelper.Init(DependencyResolver.Current.GetService<IEnumDescriptionProvider>());
        }
    }
}
