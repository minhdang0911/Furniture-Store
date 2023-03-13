using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mood.Draw;
using Mood.EF2;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class NhaCungCapController : BaseController
    {
       
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var draw = new NhaCungCapDraw();
            var model = draw.listALL(searchString,page,pagesize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBangIDUser();
            return View();
            
        }
        [HttpPost]
        public ActionResult Create(NhaCungCap model)
        {
            if(ModelState.IsValid)
            {
                var nhaCungCap = new NhaCungCapDraw();
                model.NgayTao = DateTime.Now;
                if(!nhaCungCap.CheckNCC(model.TenNCC))
                {
                    if(nhaCungCap.CheckSDT(model.SoDienThoai))
                    {
                        if(nhaCungCap.IsValidEmail(model.Email))
                        {
                            var result = nhaCungCap.Insert(model);
                            if (result > 0)
                            {
                                ModelState.AddModelError("nhacungcapSuccess", "Thêm thành công");
                            }
                            else
                            {
                                ModelState.AddModelError("nhacungcap", "Thêm thất bại");
                            }
                        }else
                        {
                            ModelState.AddModelError("nhacungcap", "Email không đúng định dạng !!!");
                        }
                    }else
                    {
                        ModelState.AddModelError("nhacungcap", "Số điện thoại không đúng định đạng !!!");
                    }
                }else
                {
                    ModelState.AddModelError("nhacungcap", "Tên nhà cung cấp đã tồn tại !!!");
                }
            }else
            {
                ModelState.AddModelError("nhacungcap", "Thêm dữ liệu không hợp lệ !!!");
            }
            SetViewBangIDUser();
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBangIDUser();
            var model = new NhaCungCapDraw().getByID(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(NhaCungCap model)
        {
            if(ModelState.IsValid)
            {
                var nhaCungCap = new NhaCungCapDraw();

                model.NgayTao = DateTime.Now;
                    if (nhaCungCap.CheckSDT(model.SoDienThoai))
                    {
                        if (nhaCungCap.IsValidEmail(model.Email))
                        {
                            var result = nhaCungCap.Update(model);
                            if (result)
                            {
                                ModelState.AddModelError("nhacungcapSuccess", "Cập nhật thành công");
                            }
                            else
                            {
                                ModelState.AddModelError("nhacungcap", "Cập nhật thất bại");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("nhacungcap", "Email không đúng định dạng !!!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("nhacungcap", "Số điện thoại không đúng định đạng !!!");
                    }
              
            }
            else
            {
                ModelState.AddModelError("nhacungcap", "Cập nhật dữ liệu không hợp lệ !!!");
            }
            SetViewBangIDUser();
            return View("Edit");

        }
        public void SetViewBangIDUser(long? selectID = null)
        {
            var dao = new NhaCungCapDraw();
            ViewBag.IDNguoiTao = new SelectList(dao.listALLUserAdmin(), "IDUSer", "UserName", selectID);
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new NhaCungCapDraw().Delete(id);
            return View();
        }
    }
}