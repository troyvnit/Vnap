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
                name: "Pages",
                url: "{attr1}/{attr2}/{attr3}/{attr4}",
                defaults: new { controller = "Home", action = "Index", attr1 = UrlParameter.Optional, attr2 = UrlParameter.Optional, attr3 = UrlParameter.Optional, attr4 = UrlParameter.Optional }
            );
        }
    }
}
