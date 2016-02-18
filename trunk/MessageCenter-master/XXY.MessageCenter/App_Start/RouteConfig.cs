using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XXY.Common.MVC;

namespace XXY.MessageCenter {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var r = routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new {
                    lang = "zh-CN",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                constraints: new {
                    lang = "(zh-CN)|(en-US)"
                }
            );

            r.RouteHandler = new MutiLangRouteHandler();

            routes.Add(new Route("{controller}/{action}/{id}",
                            new RouteValueDictionary(new {
                                lang = "zh-CN",
                                controller = "Home",
                                action = "Index",
                                id = UrlParameter.Optional
                            }), new MutiLangRouteHandler()));
        }
    }
}
