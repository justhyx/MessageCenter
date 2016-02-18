using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XXY.Common.Exceptions;

namespace XXY.MessageCenter.ExceptionHandlers {
    /// <summary>
    /// NoPermissionException 处理器
    /// </summary>
    public class NoPermissionExceptionHandler : BaseExceptionHandler {
        protected override int StatusCode {
            get {
                return 200;
            }
        }

        protected override string ViewName {
            get {
                return "Denied";
            }
        }

        public override bool CanDeal(Type exType) {
            return exType.Equals(typeof(NoPermissionException));
        }
    }
}