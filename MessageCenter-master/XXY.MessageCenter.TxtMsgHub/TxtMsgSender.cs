using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.TxtMsgHub;


[assembly: OwinStartup(typeof(TxtMsgSender))]
namespace XXY.MessageCenter.TxtMsgHub {
    public class TxtMsgSender {

        public void Configuration(IAppBuilder app) {
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new UCUserIDProvider());
            app.UseCors(CorsOptions.AllowAll);
            var cfg = new HubConfiguration() {
                EnableJSONP = true,
                EnableDetailedErrors = true
            };
            app.MapSignalR("/Msg", cfg);
            app.MapSignalR(cfg);
        }

        public static void Send(string userID, TxtMessage msg) {
            GlobalHost.ConnectionManager
                .GetHubContext<IMHub>()
                .Clients
                .User(userID)
                .newMsg(msg);
        }

    }
}
