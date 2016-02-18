using System.Web;
using System.Web.Mvc;
using XXY.MessageCenter.Filters;

namespace XXY.MessageCenter {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            filters.Add(DependencyResolver.Current.GetService<ExceptionLogAttribute>(), 1);
            filters.Add(DependencyResolver.Current.GetService<NoPermissionExceptionHandlerAttribute>(), 2);
        }
    }
}
