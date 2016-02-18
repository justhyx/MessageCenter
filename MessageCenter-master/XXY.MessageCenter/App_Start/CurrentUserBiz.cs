using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.Common.Attributes;
using XXY.MessageCenter.IBiz;
using XXY.UC.BizEntity;

namespace XXY.MessageCenter {
    /// <summary>
    /// 
    /// </summary>
    [AutoInjection(typeof(ICurrentUser))]
    public class CurrentUserBiz : ICurrentUser {

        private User DefaultUser = new User() {
            CompanyCode = "System",
            CompanyNameCN = "System",
            CompanyNameEn = "System",
            FullName = "System",
            RootCompanyNameCN = "System",
            RootCompanyNameEn = "System",
            UserName = "System"
        };

        /// <summary>
        /// 获取当前登陆的用户, 如果没有登陆, 返回一个默认的值, 用以避免 null 判断
        /// </summary>
        /// <returns></returns>
        public User GetUser() {
            //WCF 调用的时候,没有 HttpContext.Current, HttpContextHelper 设计为单元测试,有 Mock 的环境.
            //WCF 中不会有 Mock 模拟
            if (HttpContextHelper.Current != null) {
                var user = SessionHelper.Get<User>(SessionKeys.User.ToString());
                if (user == null)
                    user = this.DefaultUser;
                return user;
            } else
                return this.DefaultUser;
        }

        public bool IsLogined {
            get {
                return SessionHelper.Get<User>(SessionKeys.User.ToString()) != null;
            }
        }
    }
}
