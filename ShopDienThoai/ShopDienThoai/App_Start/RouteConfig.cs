using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopDienThoai
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "lien-he/{action}",
                defaults: new { controller = "About", action = "Index"},
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang/{action}/{id}/{quantity}",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional, quantity = UrlParameter.Optional },
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "Login", action = "Register" },
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );

            routes.MapRoute(
                name: "Category",
                 url: "danh-muc/{slug}",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );


            routes.MapRoute(
                name: "Product",
                url: "san-pham/{slug}",
                defaults: new { controller = "Product", action = "ProductDetails", slug = UrlParameter.Optional },
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );

            routes.MapRoute(
                name: "TinTuc",
                url: "tin-tuc",
                defaults: new { controller = "Articles", action = "Index"},
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );
            routes.MapRoute(
                name: "ChiTietTinTuc",
                url: "tin-tuc/{slug}",
                defaults: new { controller = "Articles", action = "ChiTietTinTuc", slug = UrlParameter.Optional },
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopDienThoai.Controllers" }
            );
        }
    }
}
