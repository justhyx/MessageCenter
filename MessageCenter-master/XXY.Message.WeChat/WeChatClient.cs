using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Extends;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.WxApi;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Messages;
using XXY.WxApi.Methods;

namespace XXY.MessageCenter.WeChat {

    [Export(typeof(IMessageClient))]
    //[ExportMetadata("MsgType", MsgTypes.WeChat)]
    public class WeChatClient : BaseMessageClient, IMessageClient {

        public event EventHandler<ProcessedArgs> OnProcessed;

        static WeChatClient() {
            var cfg = ConfigurationHelper.GetSection<WeChatConfig>();
            var configs = new List<ApiConfig>() {
                new ApiConfig("xxy", cfg.AppID, cfg.SecretKey, cfg.AesKey , cfg.Token)
            };

            ApiClient.Init(configs);
        }

        public override void Init() {
            throw new NotImplementedException();
        }

        public async Task Send(BaseMessage msg) {
            var data = (WeChatMessage)msg;
            var api = ApiClient.GetInstance("xxy");

            //接口限制: 用户不先在WX上发起对话,就没办法使用该功能!
            //{"errcode":45015,"errmsg":"response out of time limit or subscription is canceled hint: [h6kfKa0794age8]"}
            var method = new MessageSend() {
                OpenID = data.Receiver,
                Message = new TextMessage() {
                    Content = data.Ctx
                }
            };
            var result = await api.Execute(method);
            if (this.OnProcessed != null) {
                var ex = result.HasError ? new Exception(result.ErrorInfo) : null;
                this.OnProcessed(this, new ProcessedArgs(DbEntity.Enums.MsgTypes.WeChat, data.ID, ex));
            }
        }


        public Type AcceptMessageType {
            get {
                return typeof(WeChatMessage);
            }
        }
    }
}
