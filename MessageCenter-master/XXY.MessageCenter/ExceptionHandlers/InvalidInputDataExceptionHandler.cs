using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XXY.Common.Exceptions;

namespace XXY.MessageCenter.ExceptionHandlers {

    /// <summary>
    /// InvalidInputException 处理器
    /// </summary>
    public class InvalidInputDataExceptionHandler : BaseExceptionHandler {

        protected override string ViewName {
            get {
                return "InvalidInputData";
            }
        }

        public override bool CanDeal(Type exType) {
            return exType.Equals(typeof(InvalidInputException));
        }
    }
}