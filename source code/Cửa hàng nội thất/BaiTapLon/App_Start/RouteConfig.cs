using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BaiTapLon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Product Category",
                url: "danh-muc/{MetaTitle}-{idCate}",
                defaults: new { controller = "Product", action = "ListProduct", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Payment Cancel Nl",
                url: "cancel-order",
                defaults: new { controller = "Cart", action = "cancel_order", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Payment Success Nl",
                url: "confirm-orderPaymentOnline",
                defaults: new { controller = "Cart", action = "confirm_orderPaymentOnline", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Payment Success Momo",
                url: "confirm-orderPaymentOnlineMomo",
                defaults: new { controller = "Cart", action = "confirm_orderPaymentOnline_momo", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Payment Cancel Momo",
               url: "cancel-orderMomo",
               defaults: new { controller = "Cart", action = "cancel_order_momo", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Trang chu",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "TrangChu", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "About us",
                url: "gioi-thieu",
                defaults: new { controller = "AboutUs", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Promotion",
                url: "khuyen-mai",
                defaults: new { controller = "Promotion", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Product All",
                url: "san-pham",
                defaults: new { controller = "Product", action = "ListProduct", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Product detail",
                url: "chi-tiet/{MetaTitle}-{idDetail}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Register",
                url: "dang-ki",
                defaults: new { controller = "Users", action = "Register", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Payment Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Users Profile",
                url: "thong-tin-ca-nhan-{id}",
                defaults: new { controller = "Users", action = "ProfileUser", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Users Edit",
                url: "sua-thong-tin",
                defaults: new { controller = "Users", action = "EditUser", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Search Product",
                url: "tim-kiem",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Users ListProduct",
                url: "danh-sach-don-hang-{id}",
                defaults: new { controller = "Users", action = "DanhSachHang", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Users Messenger",
                url: "tin-nhan-{id}",
                defaults: new { controller = "Users", action = "MessengerUser", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Reply Messenger",
                url: "chi-tiet-lien-he-{id}",
                defaults: new { controller = "Users", action = "ChiTietLienHe", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Messenger",
                url: "phan-hoi-{id}",
                defaults: new { controller = "Users", action = "MessengerReply", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Tong quan",
                url: "tong-quan-{id}",
                defaults: new { controller = "Users", action = "Dashboard", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Users details Product",
                url: "chi-tiet-don-hang-{id}",
                defaults: new { controller = "Users", action = "ChiTietHoaDon", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Users Edit Pass",
                url: "doi-mat-khau",
                defaults: new { controller = "Users", action = "EditPassWord", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan-nhan-hang",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Payment TrucTuyen",
                url: "thanh-toan-truc-tuyen",
                defaults: new { controller = "Cart", action = "PaymentMoMo", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Contact",
                url: "Lien-He",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Users", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TrangChu", id = UrlParameter.Optional }
            );


        }
    }
}
