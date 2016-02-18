using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using XXY.Common.Extends;
using XXY.WxApi.Entities;
using XXY.WxApi.Methods;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;

namespace XXY.WxApi {
    public class ApiClient {

        /// <summary>
        /// Key : tag
        /// </summary>
        private static Dictionary<string, ApiClient> Clients = new Dictionary<string, ApiClient>();

        public ApiConfig Config {
            get;
            private set;
        }

        internal readonly CookieContainer Cookies = new CookieContainer();

        private AccessToken token = null;
        internal AccessToken Token {
            get {
                if (this.token == null)
                    this.token = new AccessToken();
                return this.token;
            }
            private set {
                this.token = value;
            }
        }





        static ApiClient() {
            //设置策略注入
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            var injector = new PolicyInjector(configurationSource);
            PolicyInjection.SetPolicyInjector(injector);
        }

        /// <summary>
        /// 初始化ApiClient 设置
        /// </summary>
        /// <param name="configs"></param>
        public static void Init(IEnumerable<ApiConfig> configs) {
            foreach (var cfg in configs) {
                Clients.Set(cfg.Tag, new ApiClient(cfg));
            }
        }

        /// <summary>
        /// 跟据Tag 获取 ApiClient 的实例
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static ApiClient GetInstance(string tag) {
            if (string.IsNullOrWhiteSpace(tag))
                return null;
            tag = tag.ToUpper();
            return Clients.Get(tag, null);
        }






        /// <summary>
        /// 获取API的URL
        /// </summary>
        /// <param name="apiName"></param>
        /// <returns></returns>
        public string GetApiUrl(string apiName) {
            return string.Format("https://api.weixin.qq.com/cgi-bin/{0}?access_token={1}", apiName, this.Token != null ? this.Token.Token : "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ApiClient(ApiConfig config) {
            if (config == null)
                throw new ArgumentNullException();

            this.Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="appID"></param>
        /// <param name="secret"></param>
        /// <param name="aesKey"></param>
        /// <param name="token"></param>
        public ApiClient(string tag, string appID, string secret, string aesKey, string token) {
            this.Config = new ApiConfig(tag, appID, secret, aesKey, token);
        }


        /// <summary>
        /// 执行API方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<T> Execute<T>(MethodBase<T> method) {
            //将方法在策略中包装
            var m = (MethodBase<T>)PolicyInjection.Wrap(method.GetType(), method);
            return await m.Execute(this);
        }


        /// <summary>
        /// 对ApiClient 进行认证
        /// </summary>
        internal async void DoAuth() {
            var method = new GetToken() {
                AppID = this.Config.AppID,
                Secret = this.Config.Secret
            };
            this.Token = await method.Execute(this);
        }


        /// <summary>
        /// 该方法返回的 ApiClient 只适用于接口调用，不能用于被动的消息回复
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="secretCode"></param>
        public static ApiClient Connect(string appID, string secretCode) {
            var client = new ApiClient("t", appID, secretCode, "", "t");
            client.DoAuth();
            if (!client.Token.IsInvalid)
                return client;
            else
                return null;
        }
    }
}
