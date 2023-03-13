using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_NganLuong
{
    public class nganluongInfo
    {
        /*
        public static readonly string Merchant_id = "49856"; // mã Merchant
        public static readonly string Merchant_password = "d70707c63edb5e9217d244dfc6b77aaa";  //Merchant password
        public static readonly string Receiver_email = "thuongdt1002@gmail.com";// email nhan tien
        //
        public static readonly string UrlNganLuong = "https://sandbox.nganluong.vn:8088/nl35/checkout.api.nganluong.post.php";
        public static readonly string return_url = "https://hieusachviet.site/confirm-orderPaymentOnline";
        // dường dẫn khi thanh tán thất bại
        public static readonly string cancel_url = "https://hieusachviet.site/cancel-order";
           */


        public static readonly string Merchant_id = "49856"; // mã Merchant
        public static readonly string Merchant_password = "d70707c63edb5e9217d244dfc6b77aaa";  //Merchant password
        public static readonly string Receiver_email = "nghiepnguyen8499@gmail.com";// email nhan tien
        public static readonly string UrlNganLuong = "https://sandbox.nganluong.vn:8088/nl35/checkout.api.nganluong.post.php";
        // dường dẫn khi thanh tán thành công
        public static readonly string return_url = "https://localhost:44350/confirm-orderPaymentOnline";
        // dường dẫn khi thanh tán thất bại
        public static readonly string cancel_url = "https://localhost:44350/cancel-order";
        
    }
}