using BaiTapLon.Common;
using BaiTapLon.Models;
using Mood.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult TrangChu()
        {
            SanphamDraw product = new SanphamDraw();
            var userSession = (UserLogin)Session[Common.Constant.USER_SESSION];
            if (userSession != null)
            {
                userSession.tolMes = new OrderDraw().getToTolMes(userSession.userId);
                userSession.tolRep = new MessengerDraw().getTotalReply(userSession.userId);
                userSession.totalMessenger = (userSession.tolMes + userSession.tolRep);
            }
            ViewBag.Sanphamnew = product.listSanphamnew(6);
            ViewBag.SanphamDeal = product.listDealPrice(6);
            ViewBag.Deal = product.listDealPrice(1);
            ViewBag.listSellings = product.listTopSellings(6);
            ViewBag.listTopRate = product.listTopSellings(5);
            ViewBag.NoiBat = product.listSanphamnew(12);
            ViewBag.listGoiY = product.listSanphamnew(5);
            ViewBag.topSelling = product.listTopSellings(20);
            var listTabCategory = product.listAllCategoreProduct();
            List<long> ListidCate = new List<long>();
            foreach (var item in listTabCategory)
            {
                if (product.getByIDCategoryTabHome(item.IDCategory).Count() >= 8)
                {
                    ListidCate.Add(item.IDCategory);
                }
            }
            ViewBag.tab1 = product.getByIDCategoryTabHome(ListidCate[0]);
            ViewBag.tab2 = product.getByIDCategoryTabHome(ListidCate[1]);
            ViewBag.tab3 = product.getByIDCategoryTabHome(ListidCate[2]);
            //ViewBag.tab4 = product.getByIDCategoryTabHome(ListidCate[3]);
            return View();

        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDraw().ListAllByID(1);
            ViewBag.Deal = new SanphamDraw().listDealPrice(6);
            ViewBag.Sanphamnew = new SanphamDraw().listSanphamnew(8);
            ViewBag.topHot = new SanphamDraw().listTopSellings(4);
            ViewBag.NoiBat = new SanphamDraw().listSanphamnew(4);
            ViewBag.getAllProduct = new SanphamDraw().getAllProduct();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult Slider()
        {
            ViewBag.slides = new SildeDraw().listALLSilde();
            return PartialView();
        }
        
        [ChildActionOnly]
        public PartialViewResult Header()
        {
           
            var cart = Session[Common.Constant.CartSession];
            var list = new List<CartItem>();
            int priceTotol = 0;
            
            if (cart != null)
            {
                list = (List<CartItem>)cart;

                foreach (var item1 in list)
                {
                    if (item1.Product.PriceSale != null)
                    {
                        int temp = (((int)item1.Product.GiaTien) - ((int)item1.Product.GiaTien / 100 * (int)item1.Product.PriceSale)) * ((int)item1.Quantity);

                        priceTotol += temp;
                    }
                    else
                    {
                        int temp = (int)item1.Product.GiaTien * (int)item1.Quantity;
                        priceTotol += temp;
                    }
                }
            }
            ViewBag.CartTotal = priceTotol;
            ViewBag.Count_ListCart = list.Count;
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDraw().ListAllByID(2);

            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var userSession = (UserLogin)Session[Common.Constant.USER_SESSION];
            var cart = Session[Common.Constant.CartSession];
            var list = new List<CartItem>();

            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            ViewBag.sessionUser = userSession;
            return PartialView(list);

        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            ViewBag.categoryFooter = new CategoryDraw().ListAllCategory(6);
            var model = new FooterDraw().getFooter();
            return PartialView(model);
        }

        
    }
}