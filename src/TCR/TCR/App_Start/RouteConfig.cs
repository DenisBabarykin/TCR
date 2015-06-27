using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TCR
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "HomeOverride",
            //    url: "Home/{*.}",
            //    defaults: new { controller = "Home", action = "Index" }
            //    );
            routes.MapRoute(
                name: "AccMan",
                url: "Account/Manage",
                defaults: new { controller = "Account", action = "Manage"}
                );

            routes.MapRoute(
                name: "HomeMenu",
                url: "Home/menu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "HomeLengthDiv",
                url: "Home/lengthDiv",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "HomeVsegDiv",
                url: "Home/vsegDiv",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "HomeDsegDiv",
                url: "Home/dsegDiv",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "HomeJsegDiv",
                url: "Home/jsegDiv",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "HomeRepertoireClones",
                url: "Home/repertoireClones",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "HomeRepertoireCountClones",
                url: "Home/repertoireCountClones",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
