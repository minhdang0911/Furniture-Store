using BaiTapLon.Areas.Admin.Models;
using BaiTapLon.Common;
using Mood.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BaiTapLon.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var draw = new UserDraw();
                var result = draw.Login(model.userName,EncryptorMD5.GetMD5(model.passWord));
                if (result == 1)
                {
                    var user = draw.getByID(model.userName);
                    var userSession = new AdminLogin();
                    userSession.userName = user.UserName;
                    userSession.userId = user.IDUser;
                    Session.Add(Constant.ADMIN_SESSION, userSession);
                    return RedirectToAction("Index", "Homes");
                }
                else if(result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản này không có quyền truy cập!!!");
                }
                else if(result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa !!!");
                }else if(result == -2)
                {
                    ModelState.AddModelError("","Mật khẩu sai!!!");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!!!");
                }
                else{
                    ModelState.AddModelError("", "Đăng nhập thất bại!!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập thất bại!!!");
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session[Constant.ADMIN_SESSION] = null;
            return View("Index");
        }
    }
}