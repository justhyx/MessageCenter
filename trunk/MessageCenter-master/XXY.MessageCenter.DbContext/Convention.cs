using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbContext {
    public class OracleConversion : Convention {

        //private static readonly Regex ConvertRx = new Regex(@"(([a-zA-Z][a-z]+)|(\d\d*)|[A-Za-z]+)");
        private static readonly Regex ConvertRx = new Regex(@"(([A-Z]+(?![a-z\d_]))|([A-Z][a-z]+)|[a-z]+|\d+)");


        public OracleConversion() {
            this.Properties().Configure(p => {
                var name = this.ConvertName(p.ClrPropertyInfo.Name);
                //var order = 10;
                var attr = p.ClrPropertyInfo.CustomAttributes.OfType<ColumnAttribute>().FirstOrDefault();
                if (attr != null && !string.IsNullOrWhiteSpace(attr.Name)) {
                    name = attr.Name;
                    //order = attr.Order;
                }
                p.HasColumnName(name);
                //p.HasColumnOrder(order);
            });

            this.Types().Configure(t => t.ToTable(this.ConvertName(t.ClrType.Name)));
        }

        private string ConvertName(string name) {
            var str = ConvertRx.Replace(name, ma => {
                return string.Format("_{0}", ma.Value);
            });
            str = Regex.Replace(str, "_{2,}", "_");
            str = str.Trim('_');
            return str.ToUpper();
        }
    }
}
