using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.UC.BizEntity;
using XXY.Common.Security;

namespace XXY.MessageCenter.TxtMsgHub {
    public class UCUserIDProvider : IUserIdProvider {
        public string GetUserId(IRequest request) {
            //不支持 Session
            //var user = SessionHelper.Get<User>("User");
            //if (user != null)
            //    return user.UserID.ToString();
            //else
            //    return string.Empty;

            var cookie = request.Cookies["MsgKey"];
            var id = AesHelper.Decrypt(cookie.Value, "56cargo.com");
            return id;
        }
    }
}
