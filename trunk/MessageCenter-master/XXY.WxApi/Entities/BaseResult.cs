using Newtonsoft.Json;

namespace XXY.WxApi.Entities {
    public class BaseResult {

        [JsonProperty("errcode")]
        public int ErrorCode {
            get;
            set;
        }

        [JsonProperty("errmsg")]
        public string ErrorInfo {
            get;
            set;
        }

        public bool HasError {
            get {
                return this.ErrorCode != 0;
            }
        }
    }
}
