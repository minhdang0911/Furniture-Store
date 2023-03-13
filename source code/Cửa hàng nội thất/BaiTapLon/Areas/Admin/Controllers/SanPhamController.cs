using ClosedXML.Excel;
using Mood.Draw;
using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
       
        public ActionResult Index(string searhString, int page = 1, int pagesize =5)
        {
            var model = new SanphamDraw().listTheloai(searhString, page, pagesize);
            return View(model);
        }
        public ActionResult LocSach(string searhString, int page = 1, int pagesize = 5)
        {
            var model = new SanphamDraw().sachcu(searhString, page, pagesize);
            return View(model);
        }
        public ActionResult LocSachHetHang(string searhString, int page = 1, int pagesize = 5)
        {
            var model = new SanphamDraw().hetHang(searhString, page, pagesize);
            return View(model);
        }
        public ActionResult TaoDon(string searhString, int page = 1, int pagesize = 5)
        {
            var model = new SanphamDraw().listProduct(searhString, page, pagesize);
            return View(model);
        }
        public ActionResult KhoHang(string searhString, int page = 1, int pagesize = 5) {
            var model = new SanphamDraw().listKhoHang(searhString, page, pagesize);
            return View(model);
            
        }
        public ActionResult ThongKeKho(string fromDate, string todate)
        {
            var thongkeList = new SanphamDraw().dataThongKe(fromDate, todate);

            List<string> listNameProduct = new List<string>();
            List<int> listSoLuong = new List<int>();
            List<int> listTonKho = new List<int>();
            foreach (var item in thongkeList)
            {
                listNameProduct.Add(item.Name);
                listSoLuong.Add(item.Soluong);
                listTonKho.Add((int)item.TonKho);
            }
            ViewBag.listNameProduct = listNameProduct;
            ViewBag.listSoLuong = listSoLuong;
            ViewBag.listTonKho = listTonKho;
            ViewBag.fromDate = fromDate;
            ViewBag.todate = todate;
            return View();
        }
        public ActionResult ExportExelTonKho(string fromDate, string toDate)
        {
           
            var wb = new XLWorkbook(@"D:\Do_An/BaiTapLon/Resource/Template/Tempalet_Kho_Hang.xlsx");
            var workSheet = wb.Worksheet(1);
            

            var list = new SanphamDraw().dataThongKe(fromDate, toDate);

            workSheet.Cell("D6").Value = DateTime.Now;


            workSheet.Cell("B16").Value = fromDate;


            workSheet.Cell("D16").Value = toDate;

            int row = 19;

            foreach (var item in list)
            {

                workSheet.Cell("B" + row).Value = item.Name;
                workSheet.Cell("C" + row).Value = item.Soluong;
                workSheet.Cell("D" + row).Value = item.TonKho;
                row++;
            }
            string nameFile = "";
            if (!string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
            {
                nameFile = "Export_Kho_Hang_Tu_" + fromDate + "_" + toDate + "_" + DateTime.Now.Ticks + ".xlsx";
            }
            else
            {
                nameFile = "Export_Tat_Ca_Kho_Hang" + "_" + DateTime.Now.Ticks + ".xlsx";
            }

            string pathFile = Server.MapPath("~/Resource/ExportFile/" + nameFile);
            try
            {
                wb.SaveAs(pathFile);
            }
            catch
            {
                ViewBag.Error("");
            }
            return Json(nameFile, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Sanpham sp)
        {
            if (ModelState.IsValid)
            {
                var draw = new SanphamDraw();
                if(draw.CheckName(sp.Name) == true)
                {
                    sp.NgayTao = DateTime.Now;
                    sp.Tophot = 1;
                    sp.Soluong = 0;
                    sp.TonKho = 0;
                    
                    var result = draw.Insert(sp);
                    if(result > 0)
                    {
                        ModelState.AddModelError("SanphamSuccess", "Thêm sách thành công");
                    }
                    else
                    {
                        ModelState.AddModelError("Sanpham", "Thêm sách thất bại!!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Sanpham", "Tên sách đã tồn tại !!!");
                }
            }
            else
            {
                ModelState.AddModelError("Sanpham", "Không thể thêm sách !!!");
            }
            SetViewBag();
           
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var draw = new SanphamDraw();
            var sanpham = draw.getByID(id);
            SetViewBag(sanpham.CategoryID);
           
            return View(sanpham);
        }
        [HttpPost]
        public ActionResult Edit(Sanpham sp)
        {
            if (ModelState.IsValid)
            {
                var draw = new SanphamDraw();
                var result = draw.Update(sp);
                if(result)
                {
                    ModelState.AddModelError("SanphamSuccess", "Cập nhật thông tin thành công");

                }
                else
                {
                    ModelState.AddModelError("Sanpham", "Cập nhật thông thất bại !!!");
                }
            }
            else
            {
                ModelState.AddModelError("Sanpham", "Không thể cập nhật thông tin !!!");
            }
            SetViewBag(sp.CategoryID);
           
            return View("Edit");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new SanphamDraw().Delete(id);
            return View();
        }
        public void SetViewBag(long? selectedId=null)
        {
           
            var dao = new CategoryDraw();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "IDCategory", "TenTheloai", selectedId);
        }

        
    }
}