using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace sideTrade.myProfilo.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Login",
            url: "Login",
            defaults: new { Controller = "Home", action = "Login" }

            );


            routes.MapRoute(
       name: "verifypassword",
       url: "verifypassword/{Code}",
       defaults: new { Controller = "Home", action = "verifypassword" }
       );


            routes.MapRoute(
                name: "Verify",
                url: "Verify/{Code}",
                defaults: new { Controller = "Home", action = "VerifyProfile" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
