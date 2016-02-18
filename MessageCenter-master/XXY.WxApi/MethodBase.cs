using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Net;
using XXY.WxApi.Attributes;

namespace XXY.WxApi {
    /// <summary>
    /// API方法基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class MethodBase<TResult> : MarshalByRefObject {

        /// <summary>
        /// 方法名
        /// </summary>
        public abstract string MethodName {
            get;
        }

        /// <summary>
        /// 请求方式
        /// </summary>
        public abstract HttpMethods RequestType {
            get;
        }

        /// <summary>
        /// API调用的结果字符串
        /// </summary>
        public string ResultString {
            get;
            protected set;
        }

        /// <summary>
        /// POST方法中,需要POST的参数
        /// </summary>
        protected abstract object PostData {
            get;
        }

        /// <summary>
        /// API调用所需要的URL参数
        /// </summary>
        /// <returns></returns>
        internal virtual Dictionary<string, string> GetParams() {
            return ParameterHelper.GetParams(this);
        }


        /// <summary>
        /// 获取API调用的结果字符串
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        internal virtual Task<string> GetResult(ApiClient client) {
            var url = client.GetApiUrl(this.MethodName);
            var dic = this.GetParams();
            var rh = new RequestHelper(client.Cookies);

            var ctx = "";
            if (this.RequestType == HttpMethods.Get) {
                ctx = rh.Get(url, dic);
            } else {
                var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this.PostData));
                ctx = rh.Post(url, dic, null, bytes, contentType: "application/json");
            }
            return Task.FromResult(ctx);
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [NeedAuth]
        public async virtual Task<TResult> Execute(ApiClient client) {
            this.ResultString = await this.GetResult(client);
            return JsonConvert.DeserializeObject<TResult>(this.ResultString);
        }
    }
}
