using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mood.Draw;
using Mood.EF2;
using BaiTapLon.Common;
using X.PagedList;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
       
        public ActionResult Index(string searhString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDraw();
            var model = dao.ListViewUser(searhString, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetQuyenAdmin();
            return View();
        }

        [HttpPost]// Post du lieu len sever
        public ActionResult Create(User entityUser)
        {
            if (ModelState.IsValid)// kiểm tra xem dữ liệu rỗng ko
            {
                var dao = new UserDraw();// tạo một đối tượng UserDraw
                if (dao.IsValidEmail(entityUser.Email) == true)
                {
                    if (dao.CheckSDT(entityUser.Phone) == true)
                    {
                        entityUser.PassWord = EncryptorMD5.GetMD5(entityUser.PassWord);// mã hóa mật khẩu khi truyền vào
                        entityUser.NgayTao = DateTime.Now;
                        if (dao.checkUserName(entityUser.UserName) == true)
                        {
                            long id = dao.Insert(entityUser); // truyền entityUser với thông tin nhận từ view xuống database
                            try
                            {
                                if (id > 0)// nếu có một user mới sẽ có một id ,  do đó nếu id >0 thì đã có user
                                {
                                    ModelState.AddModelError("CreateSuccess", "Thêm thành công ");
                                    //return RedirectToAction("Create", "User");// quay về User/create

                                }
                            }
                            catch
                            {
                                ModelState.AddModelError("CreateUser1", "Không thể thêm người dùng");
                            }
                        }

                        else
                        {
                            ModelState.AddModelError("CreateUser1", "Người dùng này đã tồn tại!!!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CreateUser1", "Số điện thoại không hợp lệ");
                    }
                }
                else
                {
                    ModelState.AddModelError("CreateUser1", "Bạn đã nhập sai định dạng email");
                }

            }
            else
            {
                ModelState.AddModelError("CreateUser1", "Thêm user thất bại !!!");
            }
            SetQuyenAdmin();
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDraw().ViewDetail(id);
            if (user.IDQuyen == 1)
            {
                SetQuyenAdmin();
            }
            else
            {
                SetQuyen();
            }
            

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User entity)
        {
            
            if (ModelState.IsValid)
            {
                var user = new UserDraw();
                if (!string.IsNullOrEmpty(entity.PassWord))
                {
                    entity.PassWord = EncryptorMD5.GetMD5(entity.PassWord);
                }
                if (user.IsValidEmail(entity.Email) == true)
                {
                    if (user.CheckSDT(entity.Phone) == true)
                    {
                        var result = user.Update(entity);
                        if (result == true)
                        {
                            ModelState.AddModelError("capnhatSuccess", "Cập nhật thành công");
                            //return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            ModelState.AddModelError("capnhat", "Cập nhật không thành công");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("capnhat", "Định dạng số điện thoại không hợp lệ!!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("capnhat", "Định dạng email không hợp lệ!!!");
                }
            }
            else
            {
                ModelState.AddModelError("capnhat", "Cập nhật thất bại!!!");
            }
            if(entity.IDQuyen== 1)
            {
                SetQuyenAdmin();
            }
            else
            {
                SetQuyen();
            }
            
            return View("Edit");
        }
        public void SetQuyen(long? selectedId = null)
        {
            var draw = new UserDraw();
            ViewBag.IDQuyen = new SelectList(draw.ListUserNomarl(), "IDQuyen", "TenQuyen", selectedId);
        }
        public void SetQuyenAdmin(long? selectedId = null)
        {
            var draw = new UserDraw();
            ViewBag.IDQuyen = new SelectList(draw.ListUserAdmin(), "IDQuyen", "TenQuyen", selectedId);
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new UserDraw().Delete(id);
            return View();
        }
        [HttpPost]// để ajax nó post
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDraw().ChangeStatus(id);
            return Json(
                new { status = result });   
        }
        

    }
}