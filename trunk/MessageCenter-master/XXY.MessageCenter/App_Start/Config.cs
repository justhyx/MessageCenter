using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using XXY.Common.Attributes;
using XXY.MessageCenter.IBiz;

namespace XXY.MessageCenter {

    [AutoInjection(typeof(IConfig))]
    public class Config : IConfig {
        public string MessageMSMQPath {
            get {
                return ConfigurationManager.AppSettings.Get("MSMQPath");
            }
        }


        public string ProcessedMessageMSMQPath {
            get {
                return ConfigurationManager.AppSettings.Get("ProcessedMSMQPath");
            }
        }
    }
}