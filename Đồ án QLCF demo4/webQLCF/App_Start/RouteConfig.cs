using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webQLCF
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
   new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            routes.MapRoute(
            name: "Login",
            url: "dang-nhap",
            defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
            namespaces: new[] { "webQLCF.Controllers" }
            );
            routes.MapRoute(
           name: "LoginForAddToCart",
           url: "dang-nhap-tai-khoan",
           defaults: new { controller = "User", action = "LoginForAddToCart", id = UrlParameter.Optional },
           namespaces: new[] { "webQLCF.Controllers" }
           );
            routes.MapRoute(
           name: "Product",
           url: "san-pham",
           defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "webQLCF.Controllers" }
           );

            routes.MapRoute(
            name: "Payment Success",
            url: "hoan-thanh",
            defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
            namespaces: new[] { "webQLCF.Controllers" }
            );
            
            routes.MapRoute(
            name: "Cart",
            url: "gio-hang",
            defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "webQLCF.Controllers" }
            );
            
            routes.MapRoute(
            name: "Add Cart",
            url: "them-gio-hang",
            defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
            namespaces: new[] { "webQLCF.Controllers" }
            );
            routes.MapRoute(
           name: "Payment",
           url: "thanh-toan",
           defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
           namespaces: new[] { "webQLCF.Controllers" }
           );
            routes.MapRoute(
           name: "PaymentError",
           url: "loi-thanh-toan",
           defaults: new { controller = "Cart", action = "Error", id = UrlParameter.Optional },
           namespaces: new[] { "webQLCF.Controllers" }
           );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "webQLCF.Controllers" }

           );
        }
    }
}
