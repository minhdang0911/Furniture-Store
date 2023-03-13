using Mood.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class HomesController : BaseController
    {
       
        public ActionResult Index()
        {
            ViewBag.coutFeedBack = new Feed_BackDraw().coutFeedBack();
            ViewBag.coutUser = new UserDraw().coutUser();
            ViewBag.coutOrder = new OrderDraw().coutOrder();
            ViewBag.coutNhapHang = new NhapHangDraw().coutNhapHang();
            return View();
        }
    }
}