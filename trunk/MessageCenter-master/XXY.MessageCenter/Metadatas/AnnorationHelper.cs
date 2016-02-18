using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Attributes;

namespace XXY.MessageCenter.Metadatas {
    public class AnnorationHelper {
        public static void AutoMap() {

            var types = typeof(AnnorationHelper).Assembly.GetTypes();
            foreach (var t in types) {
                var attr = (AnnoationForAttribute)t.GetCustomAttributes(typeof(AnnoationForAttribute), false).FirstOrDefault();
                if (attr != null)
                    TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(attr.ForType, t), attr.ForType);
            }

        }

    }
}
