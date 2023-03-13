using BaiTapLon.Areas.Admin.Models;
using BaiTapLon.Common;
using Mood.Draw;
using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class FeedBackController : BaseController
    {
     
        public ActionResult Index(string searching, int page = 1, int pagesize = 5)
        {
            var model = new Feed_BackDraw().listAllFeedBack(searching, page, pagesize);
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new Feed_BackDraw().Delete(id);
            return View();
        }

        [HttpGet]
        public ActionResult Reply(long id)
        {
            var modelFeedBack = new Feed_BackDraw().viewDetails(id);
            return View(modelFeedBack);
        }

        [HttpPost]
        public ActionResult Reply(Feed_Back modelFeedBack)
        {
            var adminSession = (AdminLogin)Session[Common.Constant.ADMIN_SESSION];
            if(ModelState.IsValid)
            {
                modelFeedBack.UpdateBy = adminSession.userName;
                var result = new Feed_BackDraw().RepLyFeedBack(modelFeedBack);
                if (result)
                {
                    ViewBag.Success = "Trả lời liên hệ thành công";
                    return View("Reply");
                }
                else
                {
                    ViewBag.Error = "Trả lời liên hệ thất bại";
                }
            }else
            {
                ViewBag.Error = "Lỗi không thể gửi yêu cầu";
            }
            return View("RepLy");
        }

        [HttpGet]
        public ActionResult ChiTiet(long id)
        {
            var modelFeedBack = new Feed_BackDraw().viewDetails(id);
            return View(modelFeedBack);
        }
    }
}