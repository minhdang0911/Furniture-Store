using ClosedXML.Excel;
using Mood.Draw;
using Mood.EF2;
using Mood.HoaDonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class HoaDonController : BaseController
    {
        // GET: Admin/HoaDon
        int soTien = 0;
        public ActionResult Index(string searhString, int page = 1, int pagesize = 5)
        {
            var hoaDon = new OrderDraw().ListALL(searhString, page, pagesize);
            return View(hoaDon);
        }

        public ActionResult XacNhan(string searhString, int page = 1, int pagesize = 5)
        {
            var hoaDon = new OrderDraw().ListALL(searhString, page, pagesize);
            return View(hoaDon);
        }
        
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new OrderDraw().Delete(id);
            return View("XacNhan", "HoaDon");
        }
        [HttpPost]// để ajax nó post
        public JsonResult ChangeStatusOrder(long id)
        {
            var result = new OrderDraw().ChangeStatusOrder(id);
            return Json(
                new { status = result });
        }

        [HttpPost]// để ajax nó post
        public JsonResult ChangeGiaoHangOrder(long id)
        {
            var result = new OrderDraw().ChangeGiaoHang(id);
            return Json(
                new { GiaoHang = result });
        }
        [HttpPost]// để ajax nó post
        public JsonResult ChangeXuatKhoOrder(long id)
        {
            var result = new OrderDraw().ChangeXuatKho(id);
            return Json(
                new { XuatKho = result });
        }
        public JsonResult ChangeSuccessOrder(long id)
        {
            
            var result = new OrderDraw().ChangeHoanThanh(id);
            return Json(
                new { NhanHang = result });
        }

        public JsonResult ChangeGiaoHangTraLai(long id)
        {
            var result = new OrderDraw().ChangeHoanThanh(id);
            return Json(
                new { NhanHang = result });
        }

        public JsonResult ChangeGiaoHangThatBai(long id)
        {
            var result = new OrderDraw().ChangeHoanThanhGiao(id);
            return Json(
                new { GiaoHang = result });
        }
        public ActionResult Details(long id, string searhString, int page = 1, int pagesize = 5)
        {
            var hoaDonModel = new OrderDraw().getOrderByID(id);
            ViewBag.hoaDonSanPham = new Order_DetailDraw().chiTietHoaDon(id, searhString, page, pagesize);
            ViewBag.total = 0;
            var listItem = new Order_DetailDraw().dataExport(id, searhString);
            foreach (var item in listItem)
            {
                ViewBag.total += (item.Price * item.Quanlity);
            }
            soTien = ViewBag.total;
            return View(hoaDonModel);
        }

        public ActionResult DongGoi(string searhString, int page = 1, int pagesize = 5)
        {
            var hoaDon = new OrderDraw().listChoGiao(searhString, page, pagesize);
            return View(hoaDon);
        }
        public ActionResult XuatKho(string searhString, int page = 1, int pagesize = 5)
        {
            var xuatkho = new OrderDraw().listXuatKho(searhString, page, pagesize);
            return View(xuatkho);
        }
        public ActionResult HoanThanh(string searhString, int page = 1, int pagesize = 5)
        {
            var hoaDon = new OrderDraw().listHoanThanh(searhString, page, pagesize);
            return View(hoaDon);
        }

        public ActionResult TraLai(string searhString, int page = 1, int pagesize = 5)
        {
            var hoaDon = new OrderDraw().listTraLai(searhString, page, pagesize);
            return View(hoaDon);
        }
        [HttpGet]
        public ActionResult GhiChu(long id)
        {
            var hoaDon = new OrderDraw().getOrderByID(id);
            ViewBag.soTien = soTien;
            return View(hoaDon);
        }
        [HttpPost]
        public ActionResult GhiChu(Orders model)
        {
            if (ModelState.IsValid)
            {
                var draw = new OrderDraw();

                var result = draw.UpdateGhiChu(model);
                if (result)
                {
                    ModelState.AddModelError("ghiChuSuccess", "Thêm ghi chú thành công");
                }
                else
                {
                    ModelState.AddModelError("ghiChu", "Thêm ghi chú thất bại");
                }
            }
            else
            {
                ModelState.AddModelError("ghiChu", "Không thể cập nhật");
            }
            return View("GhiChu");
        }
        public ActionResult ExportExel(long id)
        {
            
            var wb = new XLWorkbook(@"D:/Do_An/BaiTapLon/Resource/Template/Hoa_Don_Template.xlsx");
            var workSheet = wb.Worksheet(1);
            string searching = "";
            var list = new Order_DetailDraw().dataExport(id, searching);
            workSheet.Cell("E6").Value = list[0].OderID;
            workSheet.Cell("E7").Value = list[0].NgayTao.ToString();
            workSheet.Cell("B15").Value = list[0].TenKH;
            workSheet.Cell("B18").Value = list[0].Address;
            workSheet.Cell("F15").Value = list[0].Email;
            workSheet.Cell("D15").Value = "0" + list[0].Phone.ToString();

           

            var listItem = list[0];
            if (listItem.Status == 0)
            {
                
                workSheet.Cell("D18").Value = "Chờ Duyệt";
            }
            else
            {
               
                workSheet.Cell("D18").Value = "Đã Duyệt";
            }
            if (listItem.GiaoHang == 2)
            {
                if (listItem.NhanHang == 1)
                {
                    workSheet.Cell("F18").Value = "Đã hoàn tất";
                }
                else if (listItem.NhanHang == 2)
                {
                   
                    workSheet.Cell("F18").Value = "Trả lại hàng";
                }
                else
                {
                   
                    workSheet.Cell("F18").Value = "Đang giao hàng";
                }
            }else if(listItem.GiaoHang == 1)
            {
                workSheet.Cell("F18").Value = "Chờ Xuất Kho";
            }
            else
            {
                if(listItem.GiaoHang == 0)
                {
                    workSheet.Cell("F18").Value = "Chờ đóng gói";
                }
                if (listItem.GiaoHang == 2)
                {
                    workSheet.Cell("F18").Value = "Đã xuất kho";
                }
            }

            if (listItem.DeliveryPaymentMethod.Equals("COD"))
            {
                workSheet.Cell("B21").Value = "Tiền mặt";
            }
            else
            {
                workSheet.Cell("B21").Value = "Thanh Toán Trực Tuyến";
            }

            if (listItem.ghiChu != null)
            {
                
                workSheet.Cell("D21").Value = listItem.ghiChu;
            }
            
            int row = 24;


            for (int i = 0; i < list.Count(); i++)
            {
                workSheet.Cell("B" + row).Value = list[i].Name;
                workSheet.Cell("C" + row).Value = list[i].Quanlity;
                workSheet.Cell("D" + row).Value = list[i].Price;
                workSheet.Cell("F" + row).Value = list[i].Quanlity * list[i].Price;
                row++;

            }
            
            string nameFile = "Export_Details_Order_" + list[0].OderID + "_" + DateTime.Now.Ticks + ".xlsx";
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
    }
}