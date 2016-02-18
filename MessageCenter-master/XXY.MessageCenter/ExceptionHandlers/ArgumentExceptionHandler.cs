using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XXY.MessageCenter.ExceptionHandlers {
    /// <summary>
    /// 参数异常处理器
    /// </summary>
    public class ArgumentExceptionHandler : BaseExceptionHandler {
        protected override int StatusCode {
            get {
                return 200;
            }
        }

        protected override string ViewName {
            get {
                return "ArgumentException";
            }
        }

        public override bool CanDeal(Type exType) {
            return exType.Equals(typeof(ArgumentException));
        }
    }
}