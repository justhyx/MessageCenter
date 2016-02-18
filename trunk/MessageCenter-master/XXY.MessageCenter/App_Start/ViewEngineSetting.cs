using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XXY.MessageCenter {
    /// <summary>
    /// 视图引擎设置
    /// </summary>
    public class ViewEngingSetting {

        public static void Config() {
            //只使用 Razor , 只使用 cshtml 文件 
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine() {
                AreaViewLocationFormats = new string[] { 
                    "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                    "~/Areas/{2}/Views/Shared/{0}.cshtml" ,
                    "~/Views/Shared/{0}.cshtml" 
                },
                AreaMasterLocationFormats = new string[] { 
                    "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                    "~/Areas/{2}/Views/Shared/{0}.cshtml" ,
                    "~/Views/Shared/{0}.cshtml" 
                },
                AreaPartialViewLocationFormats = new string[] { 
                    "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                    "~/Areas/{2}/Views/Shared/{0}.cshtml" ,
                    "~/Views/Shared/{0}.cshtml"  ,
                },
                ViewLocationFormats = new string[] { 
                    "~/Views/{1}/{0}.cshtml", 
                    "~/Views/Shared/{0}.cshtml" ,
                    "~/Views/Shared/Exceptions/{0}.cshtml"
                },
                MasterLocationFormats = new string[] { 
                    "~/Views/{1}/{0}.cshtml", 
                    "~/Views/Shared/{0}.cshtml" },
                PartialViewLocationFormats = new string[] { 
                    "~/Views/{1}/{0}.cshtml", 
                    "~/Views/Shared/{0}.cshtml" ,
                    "~/Views/Shared/Tpl/{0}.cshtml",
                    "~/Views/Shared/Exceptions/{0}.cshtml"
                },
                FileExtensions = new string[] { "cshtml" }
            });
        }

    }
}