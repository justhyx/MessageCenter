using RazorEngine;
using RazorEngine.Compilation;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Security;

namespace XXY.MessageCenter.TemplateParser {
    public static class StringHelper {

        public static string Razor(this string template, object model) {
            var m = RazorDynamicObject.Create(model, true);

            var key = template.To16bitMD5();
            if (Engine.Razor.IsTemplateCached(key, null)) {
                return Engine.Razor.Run(key, null, m);
            } else {
                return Engine.Razor.RunCompile(template, key, null, m);
            }
        }

    }
}
