using Mood.Draw;
using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class WebManagerController : BaseController
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu(string searhString, int page = 1, int pagesize = 5)
        {
            var modelMenu = new MenuDraw().listMenu(searhString, page, pagesize);
            return View(modelMenu);
        }
        [HttpGet]
        public ActionResult MenuCreate()
        {
            setViewBagMenu();
            return View();
        }

        [HttpPost]
        public ActionResult MenuCreate(Menu modelMenu, string Target2)
        {
            if (ModelState.IsValid)
            {
                var draw = new MenuDraw();
                if (!draw.checkNameMenu(modelMenu.NameMenu))
                {
                    modelMenu.CreateDate = DateTime.Now;
                    if (Target2.Equals("1"))
                    {
                        modelMenu.Target = "_blank";
                    }
                    else
                    {
                        modelMenu.Target = "_self";
                    }
                    var result = new MenuDraw().InsertMenu(modelMenu);
                    if (result > 0)
                    {
                        ModelState.AddModelError("menuSuccess", "Tạo mới menu thành công");
                    }
                    else
                    {
                        ModelState.AddModelError("menu", "Không thể tạo menu mới!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("menu", "Tên menu đã tồn tại !!!");
                }
            }
            else
            {
                ModelState.AddModelError("menu", "Không thể tạo menu mới!!");
            }
            setViewBagMenu();
            return View("MenuCreate");
        }
        [HttpGet]
        public ActionResult MenuEdit(long id)
        {
            setViewBagMenu();
            var modelMenu = new MenuDraw().viewDetails(id);
            ViewBag.target = modelMenu.Target;
            return View(modelMenu);
        }
        [HttpPost]
        public ActionResult MenuEdit(Menu modelMenu, string Target2)
        {
            var model = new MenuDraw().viewDetails(modelMenu.IDMenu);
            ViewBag.target = model.Target;
            if (ModelState.IsValid)
            {
                var draw = new MenuDraw();

                modelMenu.CreateDate = DateTime.Now;
                if (Target2.Equals("1"))
                {
                    modelMenu.Target = "_blank";
                }
                else
                {
                    modelMenu.Target = "_self";
                }
                var result = new MenuDraw().UpdateMenu(modelMenu);

                if (result)
                {
                    ModelState.AddModelError("menuSuccess", "Cập nhật menu thành công");
                }
                else
                {
                    ModelState.AddModelError("menu", "Không thể cập nhật menu!!");
                    
                }
            }
            else
            {
                ModelState.AddModelError("menu", "Không thể cập nhật menu!!");
                
            }
            setViewBagMenu();
            return View("MenuEdit");
        }

        public void setViewBagMenu(long? selectedId = null)
        {
            var dao = new MenuDraw();
            ViewBag.MenuTypeID = new SelectList(dao.listAllMenuType(), "MenuTypeID", "NameType", selectedId);// bên trong chưa 3 tham số list, giá trị ID, tên hiển thị
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new MenuDraw().Delete(id);
            return View();
        }
        public ActionResult Silder(string searhString, int page = 1, int pagesize = 5)
        {
            var modelSilder = new SildeDraw().listSilderView(searhString, page, pagesize);
            return View(modelSilder);
        }
        [HttpGet]
        public ActionResult SliderCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SliderCreate(Slide model)
        {
            if(ModelState.IsValid)
            {
                var draw = new SildeDraw();
                if(!draw.checkThuTu(model.DisPlayOrder))
                {
                    var result = new SildeDraw().InsertSlider(model);
                    if(result > 0)
                    {
                        ModelState.AddModelError("sliderSuccess", "Thêm mới slider thành công");
                    }else
                    {
                        ModelState.AddModelError("slider", "Không thể thêm mới slider!!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("slider", "Thứ tự này đã có siler khác!!!");
                }
            }else
            {
                ModelState.AddModelError("slider", "Không thể thêm mới silder!!!");
            }
            return View("SliderCreate");
        }

        [HttpDelete]
        public ActionResult DeleteSlider(long id)
        {
            new SildeDraw().DeleteSlider(id);
            return View();
        }
        [HttpGet]
        public ActionResult SliderEdit(long id)
        {
            var modelSlider = new SildeDraw().viewDetails(id);
            return View(modelSlider);
        }
        [HttpPost]
        public ActionResult SliderEdit(Slide model)
        {
            if (ModelState.IsValid)
            {
                var draw = new SildeDraw();
                
                    var result = new SildeDraw().UpdateSilder(model);
                    if (result)
                    {
                        ModelState.AddModelError("sliderSuccess", "Cập nhật slider thành công");
                    }
                    else
                    {
                        ModelState.AddModelError("slider", "Không thể cập nhật slider!!!");
                    }
               
            }
            else
            {
                ModelState.AddModelError("slider", "Không thể cập nhật silder!!!");
            }
            return View("SliderEdit");
        }
    }
}