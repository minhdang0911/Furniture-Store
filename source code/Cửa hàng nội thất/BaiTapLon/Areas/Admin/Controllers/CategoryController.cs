using Mood.Draw;
using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BaiTapLon.Common;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
       
        public ActionResult Index(string searhString, int page=1,int pagesize=5)
        {
            var draw = new CategoryDraw();
            var model = draw.ListAll(searhString, page, pagesize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            var adminSession = (AdminLogin)Session[Constant.ADMIN_SESSION];
            if (ModelState.IsValid)
            {
                var draw = new CategoryDraw();
                model.NgayTao = DateTime.Now;
                model.NguoiTao = adminSession.userName;
                if(draw.CheckTheloai(model.TenTheloai) == false)
                {
                    var result = draw.Insert(model);
                    if (result > 0)
                    {
                        ModelState.AddModelError("categorySuccess", "Thêm thành công");
                    }
                    else
                    {
                        ModelState.AddModelError("category", "Thêm thất bại");
                    }
                }
                else
                {
                    ModelState.AddModelError("category", "Tên thể loại đã tồn tại!!!");
                }
                
            }
            else
            {
                ModelState.AddModelError("category", "Thêm thất bại");
            }
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = new CategoryDraw().ViewDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            if(ModelState.IsValid)
            {
                var draw = new CategoryDraw();
                
                    var result = draw.Update(model);
                    if (result)
                    {
                        ModelState.AddModelError("categorySuccess", "Cập nhật thành công");
                    }
                    else
                    {
                        ModelState.AddModelError("category", "Cập nhật thất bại");
                    }
                }               
            else
            {
                ModelState.AddModelError("category", "Không thể cập nhật");
            }
            return View("Edit");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new CategoryDraw().Delete(id);
            var listDanhSachDel = new SanphamDraw().listDelByCategory(id);
            foreach(var item in listDanhSachDel)
            {
                new SanphamDraw().Delete(item.IDContent);
            }
            return View();
        }
    }
}