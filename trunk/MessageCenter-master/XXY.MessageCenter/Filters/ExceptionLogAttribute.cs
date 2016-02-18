using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XXY.Common;
using XXY.Common.Extends;
using XXY.MessageCenter.ExceptionHandlers;

namespace XXY.MessageCenter.Filters {

    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class ExceptionLogAttribute : FilterAttribute, IExceptionFilter {

        [Dependency]
        public ILog Log {
            get;
            set;
        }

        /// <summary>
        /// 异常处理器
        /// </summary>
        private static IEnumerable<BaseExceptionHandler> Handlers;

        static ExceptionLogAttribute() {
            //异常处理器都需要扩展自 BaseExceptionHandler
            Handlers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsPublic && t.GetConstructor(Type.EmptyTypes) != null && typeof(BaseExceptionHandler).IsAssignableFrom(t))
                .Select(t => (BaseExceptionHandler)Activator.CreateInstance(t));
        }

        public void OnException(ExceptionContext filterContext) {

            var ex = filterContext.Exception.GetBaseException();
            //可以处理该异常的处理器
            var canDealHandlers = Handlers.Where(h => h.CanDeal(ex.GetBaseException().GetType())).ToList();

            if (canDealHandlers.Count > 0)
                canDealHandlers.ForEach(h => {
                    h.DealException(filterContext, this.Log);
                });
            else {
                BaseExceptionHandler.DefaultHandler.DealException(filterContext, this.Log);
            }
        }
    }
}