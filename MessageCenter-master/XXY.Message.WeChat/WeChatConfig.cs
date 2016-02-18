using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.WeChat {
    public class WeChatConfig : ConfigurationSection {
        #region 配置節設置，設定檔中有不能識別的元素、屬性時，使其不報錯
        /// <summary>
        /// 遇到未知屬性時，不報錯
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value) {
            //return base.OnDeserializeUnrecognizedAttribute(name, value);
            return true;
        }

        /// <summary>
        /// 遇到未知元素時，不報錯
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader) {
            //return base.OnDeserializeUnrecognizedElement(elementName, reader);
            return true;
        }

        #endregion

        /// <summary>
        /// 系统代码
        /// </summary>
        [ConfigurationProperty("appID", IsRequired = true)]
        public string AppID {
            get {
                return this["appID"].ToString();
            }
            set {
                this["appID"] = value;
            }
        }

        [ConfigurationProperty("secretKey")]
        public string SecretKey {
            get {
                return this["secretKey"].ToString();
            }
            set {
                this["secretKey"] = value;
            }
        }


        [ConfigurationProperty("token")]
        public string Token {
            get {
                return this["token"].ToString();
            }
            set {
                this["token"] = value;
            }
        }

        [ConfigurationProperty("aesKey")]
        public string AesKey {
            get {
                return this["aesKey"].ToString();
            }
            set {
                this["aesKey"] = value;
            }
        }
    }
}
