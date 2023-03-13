using BaiTapLon.Models;
using Mood.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Controllers
{
    public class ProductController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Category()
        {
            var cart = Session[Common.Constant.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            ViewBag.CartList = list.Count;
            
            var model = new CategoryDraw().ListAllCategory(7);
            ViewBag.Sanphamnew = new SanphamDraw().listSanphamnew(8);
            ViewBag.topHot = new SanphamDraw().listTopSellings(8);
            ViewBag.getAllProduct = new SanphamDraw().getAllProduct();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult CategoryMobile()
        {
            ViewBag.listMenu = new MenuDraw().listAll();
            var model = new CategoryDraw().ListAllCategory(7);
            ViewBag.getAllProduct = new SanphamDraw().getAllProduct();
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new SanphamDraw().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyWord, int page = 1, int pagesize = 20)
        {
            var model = new SanphamDraw().getByKeyWord(keyWord, page, pagesize);
            ViewBag.totalKq = model.Count();
            ViewBag.keyWord = keyWord;
            ViewBag.listGoiY = new SanphamDraw().listSanphamnew(5);
            ViewBag.total = new SanphamDraw().ListCount();
            ViewBag.Category = new CategoryDraw().ListAll();
            return View(model);
        }
        public ActionResult ListProduct(long? idCate,int page = 1, int pageSize = 20)
        {
            IEnumerable<Mood.EF2.Sanpham> model;
            if(idCate != null)
            {
                model = new SanphamDraw().getByIDcate(idCate,page, pageSize);
                ViewBag.TenTheLoai = new CategoryDraw().getByID((int)idCate);
            }
            else
            {
                model = new SanphamDraw().listAllProduct(page, pageSize);
            }
            ViewBag.listGoiY = new SanphamDraw().listSanphamnew(5);
            ViewBag.total = new SanphamDraw().ListCount();
            ViewBag.Category = new CategoryDraw().ListAll();
            return View(model);
        }
        
        public ActionResult Detail(long idDetail)
        {
            var model = new SanphamDraw().getByID(idDetail);
            ViewBag.sanPhamCategory = new CategoryDraw().getByID(model.CategoryID.Value);
            ViewBag.sanPhamLienquan = new SanphamDraw().getByIDcateDetail(model.CategoryID, idDetail);
            return View(model);
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}