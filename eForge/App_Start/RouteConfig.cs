using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eForge {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ThoughtsByCategory",
                url: "Thoughts/Category/{category}",
                defaults: new { controller = "Thoughts", action = "Category" }
            );

            //routes.MapRoute(
            //    name: "ImageDisplay",
            //    url: "Images/GetImage/{guid}",
            //    defaults: new { controller = "Images", action = "GetImage" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
