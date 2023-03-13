using BaiTapLon.Common;
using Mood.Draw;
using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Controllers
{
    public class ContactController : Controller
    {
        
        public ActionResult Index()
        {
            var draw = new ContactDraw().getActiveContent();
            var sessionUser = (UserLogin)Session[Constant.USER_SESSION];
            if(sessionUser!=null)
            {
                var userLogin = new UserDraw().getByIDLogin(sessionUser.userId);
                ViewBag.LoginUser = userLogin;
            }
            return View(draw);
        }
        public JsonResult Send(string name, string phone, string email, string address,string tieude, string msg)
        {
            var feedBack = new Feed_Back();
            feedBack.Name = name;
            feedBack.Phone = phone;
            feedBack.Email = email;
            feedBack.Address = address;
            feedBack.TieuDe = tieude;
            feedBack.Content = msg;
            var userSession = (UserLogin)Session[Constant.USER_SESSION];
            if(userSession!=null)
            { feedBack.CustomerID = userSession.userId; }    
            feedBack.CreateDate = DateTime.Now;
            feedBack.Status = false;
            var resultID = new ContactDraw().InserFeebBack(feedBack);
            if (resultID > 0)
            {
                return Json(new {
                    status = true
                });
              
            }else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}