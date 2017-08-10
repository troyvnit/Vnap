using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vnap.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "Home",
                 url: "",
                 defaults: new { controller = "Home", action = "Index" }
             );

            routes.MapRoute(
                name: "Home Index",
                url: "Home/{act}",
                defaults: new { controller = "Home", action = "Index", act = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Pages",
                url: "{attr1}/{attr2}/{attr3}/{attr4}",
                defaults: new { controller = "Admin", action = "Index", attr1 = UrlParameter.Optional, attr2 = UrlParameter.Optional, attr3 = UrlParameter.Optional, attr4 = UrlParameter.Optional }
            );
        }
    }
}
