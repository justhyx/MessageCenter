using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.IService;
using XXY.MessageCenter.TemplateParser;

namespace XXY.MessageCenter.Tests {

    [TestClass]
    public class TemplateParserTest {

        [TestMethod]
        public void Parse() {
            var tmp = @"Hello @Model.Name @Model.Sex";

            var o = new {
                Name = "xling"
            };

            var a = tmp.Razor(o);
        }

        [Serializable]
        public class Test {
            public string Name {
                get;
                set;
            }

            public int Age {
                get;
                set;
            }
        }
    }
}
