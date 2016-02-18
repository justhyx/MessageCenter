using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.IBiz;
using XXY.UC.BizEntity;

namespace XXY.MessageCenter.Biz {
    public abstract class BaseBiz : IBaseBiz {

        public Lazy<User> User = new Lazy<User>(() => {
            return ServiceLocator.Current.GetInstance<ICurrentUser>().GetUser();
        });

        public bool HasError {
            get {
                return this.Errors.Count > 0;
            }
        }

        private Dictionary<string, string> errors = new Dictionary<string, string>();
        public Dictionary<string, string> Errors {
            get {
                if (this.errors == null)
                    this.errors = new Dictionary<string, string>();
                return this.errors;
            }
            set {
                this.errors = value;
            }
        }
    }

    internal static class BaseBizHelper {
        public static void SetModifyInfo(this BaseBiz biz, BaseEntity data) {
            data.ModifyByUserID = biz.User.Value.UserID;
            data.ModifyByUserName = biz.User.Value.UserName;
            data.ModifyOn = DateTime.Now;
        }

        public static void SetCreateInfo(this BaseBiz biz, BaseEntity data) {
            data.CreateByUserID = biz.User.Value.UserID;
            data.CreateByUserName = biz.User.Value.UserName;
            data.CreateOn = DateTime.Now;
        }
    }
}
