using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiTapLon.Models;
using Mood.Draw;
using BaiTapLon.Common;
using Mood.EF2;
using BotDetect.Web.Mvc;
using Mood.HoaDonModel;
using Facebook;
using System.Configuration;
using System.Text;
using CommomSentMail;

namespace BaiTapLon.Controllers
{
    public class UsersController : Controller
    {
        private const string CartSession = "CartSession";
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

       

        [HttpPost]
        public JsonResult RegisterUser(RegisterModel model)
        {
            int result = 0;
            if (ModelState.IsValid)
            {
                var dao = new UserDraw();
                if (model.PassWord.Contains(model.ConfirmPass) == true)
                {
                    if (dao.IsValidEmail(model.Email) == true)
                    {
                        if (dao.CheckSDT(model.Phone) == true)
                        {
                            if (dao.checkUserName(model.UserName) == true)
                            {
                                if (dao.checkMailUser(model.Email) == false)
                                {
                                    var user = new User();
                                    user.PassWord = EncryptorMD5.GetMD5(model.PassWord);
                                    user.NgayTao = DateTime.Now;
                                    user.UserName = model.UserName;
                                    user.Email = model.Email;
                                    user.Adress = model.Address;
                                    user.Name = model.Name;
                                    user.Phone = model.Phone;
                                    user.Status = true;
                                    user.IDQuyen = 2;
                                    long id = dao.Insert(user); 
                                    try
                                    {
                                        if (id > 0)
                                        {

                                            model = new RegisterModel();
                                        
                                            result = 1;

                                        }
                                        else
                                        {

                                            result = 0;
                                            
                                        }
                                    }
                                    catch
                                    {

                                        result = 0;
                                        
                                    }
                                }
                                else
                                {

                                    result = -1;
                                }
                            }

                            else
                            {

                                result = -2;
                               
                            }
                        }
                        else
                        {

                            result = -3;
                           
                        }
                    }
                    else
                    {

                        result = -4;
                       
                    }
                }
                else
                {
                    result = -5;
                }


            }
            else
            {
                ViewBag.Error = "Tạo tài khoản thất bại !!!";
                result = 0;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [ChildActionOnly]
        public PartialViewResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult Login(LoginModel model)
        {
            var draw = new UserDraw();
            var result = 5;
            if (ModelState.IsValid)
            {
                result = draw.LoginHomeUser(model.UserName, EncryptorMD5.GetMD5(model.PassWord));
                if (result == 1)
                {

                    var user = draw.getByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.userName = user.UserName;
                    userSession.name = user.Name;
                    userSession.address = user.Adress;
                    userSession.userId = user.IDUser;
                    Session.Add(Constant.USER_SESSION, userSession);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            Session[Common.Constant.USER_SESSION] = null;
            Session[CartSession] = null;
            return Redirect("/");
        }
        public ActionResult ProfileUser()
        {
            var userProfile = (UserLogin)Session[Common.Constant.USER_SESSION];
            
            if (userProfile != null)
            {
                var userModel = new UserDraw().getIDByUser(userProfile.userId);
                userProfile.tolMes = new OrderDraw().getToTolMes(userProfile.userId);
                userProfile.tolRep = new MessengerDraw().getTotalReply(userProfile.userId);
                userProfile.totalMessenger = (userProfile.tolMes + userProfile.tolRep);
                return View(userModel);
            }

            return View();
        }
        public ActionResult Dashboard()
        {
            var userProfile = (UserLogin)Session[Common.Constant.USER_SESSION];

            if (userProfile != null)
            {
                var userModel = new UserDraw().getIDByUser(userProfile.userId);
                userProfile.tolMes = new OrderDraw().getToTolMes(userProfile.userId);
                userProfile.tolRep = new MessengerDraw().getTotalReply(userProfile.userId);
                userProfile.totalMessenger = (userProfile.tolMes + userProfile.tolRep);
                return View(userModel);
            }

            return View();
        }
        
        [HttpPost]
        public JsonResult EditUser(User entity)
        {
            int data = 0;
            var userChange = new UserDraw().getIDByUser(entity.IDUser);
            userChange.Name = entity.Name;
            userChange.Phone = entity.Phone;
            userChange.Email = entity.Email;
            userChange.Adress = entity.Adress;
            if (Session[Constant.USER_SESSION] != null)
            {
                var user = new UserDraw();
                    
                    if (user.IsValidEmail(entity.Email) == true)
                    {
                        if (user.CheckSDT(entity.Phone) == true)
                        {
                            var result = user.UpdateUser(userChange);
                            if (result == true)
                            {
                                var userAdd = new UserDraw().getByID(userChange.UserName);
                                var userSession = new UserLogin();
                                userSession.userName = userAdd.UserName;
                                userSession.name = userAdd.Name;
                                userSession.address = userAdd.Adress;
                                userSession.userId = userAdd.IDUser;
                                Session.Add(Constant.USER_SESSION, userSession);
                                ViewBag.Success = "Cập nhật thành công";
                                data = 1;
                               
                            }
                            else
                            {
                                ViewBag.Error = "Cập nhật không thành công";
                                data = 0;
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Định dạng số điện thoại không hợp lệ!!!";
                            data = -1;
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Định dạng email không hợp lệ!!!";
                        data = -2;
                    }
                
                
                
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult EditPassWord(RestestPassModel modelPass)
        {
            int data = 0;
            var userChange = new UserDraw().getIDByUser(modelPass.IDUser);
            
            if (Session[Constant.USER_SESSION] != null)
            {
                if (ModelState.IsValid)
                {
                    var user = new UserDraw();
                    if (!string.IsNullOrEmpty(userChange.PassWord))
                    {
                        if(modelPass.oldPassWord != null || modelPass.oldPassWord != null)
                        {
                            if (userChange.PassWord.Contains(EncryptorMD5.GetMD5(modelPass.oldPassWord)))
                            {
                                if (modelPass.newPassWorrd.Contains(modelPass.confinrmPass))
                                {
                                    var result = user.UpdatePassword(userChange, EncryptorMD5.GetMD5(modelPass.newPassWorrd));
                                    if (result == true)
                                    {
                                        ViewBag.Success = "Đổi mật khẩu thành công";
                                        data = 1;
                                    
                                    }
                                    else
                                    {
                                        ViewBag.Error = "Cập nhật không thành công";
                                        data = 0;
                                    }
                                }
                                else
                                {
                                    ViewBag.Error = "Mật khẩu mới không trùng khớp";
                                    data = -2;
                                }
                            }
                            else
                            {
                                ViewBag.Error = "Mật khẩu cũ không chính xác";
                                data = -3;
                            }
                        }
                        else
                        {
                            data = -4;
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Mật khẩu cũ không được để trống";
                        data = -1;
                    }

                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DanhSachHang(long id, string searhString, int page = 1, int pagesize = 5)
        {
            if (Session[Constant.USER_SESSION] != null)
            {
                var orderUser = new UserDraw().ListALLHoaDonUSer(id, searhString, page, pagesize);
                return View(orderUser);
            }
            return View();
        }
        [HttpPost]
        public JsonResult ChangeSuccessOrder(long id)
        {
            var result = new OrderDraw().ChangeHoanThanh(id);
            return Json(
                new { NhanHang = result });
        }
        [HttpGet]
        public ActionResult ChiTietHoaDon(long id, int page = 1, int pagesize = 20)
        {
            if (Session[Constant.USER_SESSION] != null)
            {
                var hoaDonModel = new OrderDraw().getOrderByID(id);
                ViewBag.hoaDonSanPham = new Order_DetailDraw().chiTietHoaDonUser(id, page, pagesize);
                int priceTotol = 0;
                var listItem = new Order_DetailDraw().dataExport(id,"");
                foreach (var item1 in listItem)
                {
                      int temp = (int)item1.Price * (int)item1.Quanlity;
                      priceTotol += temp;
                    
                }
                ViewBag.priceTotol = priceTotol;
                return View(hoaDonModel);
            }
            return View();
        }

        [HttpGet]
        public ActionResult MessengerUser(long id, string searhString, int page = 1, int pagesize = 5)
        {
            if (Session[Constant.USER_SESSION] != null)
            {

                var orderUser = new MessengerDraw().listTinNhanUser(id, searhString, page, pagesize);

                return View(orderUser);

            }
            return View();
        }
        [HttpGet]
        public ActionResult MessengerReply(long id, string searhString, int page = 1, int pagesize = 5)
        {
            if (Session[Constant.USER_SESSION] != null)
            {

                var feedBack = new MessengerDraw().listTinNhanFeedBack(id, searhString, page, pagesize);

                return View(feedBack);

            }
            return View();
        }
        public ActionResult ChiTietLienHe(long id)
        {
            var userReply = new Feed_BackDraw().viewDetails(id);
            return View(userReply);
        }
        [AllowAnonymous]
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginFb = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FBAppId"],
                client_secret = ConfigurationManager.AppSettings["FBAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginFb.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FBAppId"],
                client_secret = ConfigurationManager.AppSettings["FBAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code,
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                //Get ra thông tin cần sử dung
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;

                var user = new User();
                user.Email = email;
                user.UserName = email;
                user.Name = firstName + " " + middleName + " " + lastName;


                var addUserr = new UserDraw().InsertFaceBook(user);
                if (addUserr > 0)
                {
                    var usergetByID = new UserDraw().getByIDLogin(addUserr);
                    var userSession = new UserLogin();
                    userSession.userName = usergetByID.UserName;
                    userSession.name = usergetByID.Name;
                    userSession.address = usergetByID.Adress;
                    userSession.userId = usergetByID.IDUser;
                    Session.Add(Constant.USER_SESSION, userSession);
                    //Session[CartSession] = null;
                    return Redirect("/");
                }
                else
                {

                }
            }
            else
            {

            }
            return Redirect("/");
        }
        
        [HttpPost]
        public JsonResult RetestPassWord(string emailRetestPass, string userRetestPass)
        {
            int result = 0;
            if (ModelState.IsValid)
            {
                result = 0;
                var passRetest = new UserDraw();
                if (passRetest.IsValidEmail(emailRetestPass))
                {
                    if (passRetest.checkMailUserRetest(emailRetestPass, userRetestPass))
                    {
                        var userRetest = new UserDraw().getbyEmail(emailRetestPass);
                        Random rand = new Random();
                        StringBuilder passBuider = new StringBuilder(7);
                        for (int i = 0; i < 7; i++)
                        {
                            passBuider.Append(rand.Next(1, 10).ToString());
                        }

                        userRetest.PassWord = EncryptorMD5.GetMD5(passBuider.ToString());
                        if (passRetest.UpdatePass(userRetest))
                        {
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Home/template/retestPass.html"));
                            content = content.Replace("{{pasNew}}", passBuider.ToString());
                            new MailHelper().sentMail(emailRetestPass, "Yêu cầu thay đổi mật khẩu", content);
                            result = 1;
                            ViewBag.Success = "Chúng tôi đã gửi một email mật khẩu mới đến hòm thư của bạn, Vui lòng kiểm tra trong hòm thư";

                        }
                        else
                        {
                            result = -1;
                            ViewBag.Error = "Không cấp được mật khẩu";
                        }

                    }
                    else
                    {
                        result = -2;
                        ViewBag.Error = "Không tìm thấy tài khoản nào!!!";
                    }
                }
                else
                {
                    result = -3;
                    ViewBag.Error = "Email nhập không đúng định đạng !!!";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
    }
}