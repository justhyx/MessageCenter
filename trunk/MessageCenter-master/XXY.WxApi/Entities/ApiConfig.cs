using System;

namespace XXY.WxApi.Entities {
    public class ApiConfig {

        /// <summary>
        /// 用于标记不同的 微信公众号，应在入处加上 tag 参数
        /// Tag 必须唯一
        /// </summary>
        public string Tag {
            get;
            set;
        }

        public string AppID {
            get;
            set;
        }

        public string Secret {
            get;
            set;
        }

        public string AesKey {
            get;
            set;
        }

        public string Token {
            get;
            set;
        }

        public ApiConfig(string tag, string appID, string secret, string aesKey, string token) {
            if (string.IsNullOrWhiteSpace(tag)
                || string.IsNullOrWhiteSpace(appID)
                || string.IsNullOrWhiteSpace(secret)
                //|| string.IsNullOrWhiteSpace(aesKey)
                || string.IsNullOrWhiteSpace(token)
                )
                throw new ArgumentNullException();

            this.Tag = tag.ToUpper();
            this.AppID = appID;
            this.Secret = secret;
            this.AesKey = aesKey;
            this.Token = token;
        }
    }
}
