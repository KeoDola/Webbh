using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSiteBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
      new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
              name: "DanhSachSanPham",
              url: "danh-sach-san-pham/{id}",
              defaults: new { controller = "DanhSachSanPham", action = "Index" }
          );

            routes.MapRoute(
                name: "ChiTietSanPham",
                url: "chi-tiet-san-pham/{id}",
                defaults: new { controller = "ChiTietSanPham", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
