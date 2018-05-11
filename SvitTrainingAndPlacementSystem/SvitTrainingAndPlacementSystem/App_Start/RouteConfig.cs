using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SvitTrainingAndPlacementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "First", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //     name: "routeOne",
            //     url: "Company/Index",
            //    defaults: new { controller = "Company", action = "Index" });

        }
    }
}
