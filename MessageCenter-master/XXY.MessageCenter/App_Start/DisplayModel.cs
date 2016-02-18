using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.WebPages;
using XXY.Common.Extends;

namespace XXY.MessageCenter {
    public static class DisplayModelConfig {

        public static void Config() {

            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("en-US") {
                ContextCondition = ctx => {
                    var data = RouteTable.Routes.GetRouteData(ctx);
                    if (data != null) {
                        var lang = (string)data.Values.Get("lang", ""); //["lang"];
                        return string.Equals(lang, "en-US", StringComparison.OrdinalIgnoreCase);
                    }
                    return false;
                }
            });
        }


    }
}
