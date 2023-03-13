using ClosedXML.Excel;
using Mood.Draw;
using Mood.EF2;
using Mood.NhapHangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class NhapHangController : BaseController
    {
        
        public ActionResult Index(string searhString, int page = 1, int pagesize = 5)
        {
            var model = new NhapHangDraw().nhapHangView(searhString, page, pagesize);
            return View(model);
        }
        public ActionResult Duyet(string searhString, int page = 1, int pagesize = 5)
        {
            var model = new NhapHangDraw().DuyetView(searhString, page, pagesize);
            return View(model);
        }

        public ActionResult NhapKho(string searhString, int page = 1, int pagesize = 5)
        {
            var model = new NhapHangDraw().nhapKhoView(searhString, page, pagesize);
            return View(model);
        }
        public ActionResult HoanThanh(string searhString, int page = 1, int pagesize = 5)
        {
            var model = new NhapHangDraw().nhapHoanThanhView(searhString, page, pagesize);
            return View(model);
        }
        [HttpGet]
        public ActionResult LapPhieuNhap(long id)
        {
            var draw = new SanphamDraw();
            var sanpham = draw.getByID(id);

            SetViewNhaCungCap();
            return View(sanpham);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var donNhap = new NhapHangDraw().getById(id);
            return View(donNhap);
        }
        [HttpPost]
        public ActionResult Edit(NhapHangModelView model, string payment)
        {

            if (ModelState.IsValid)
            {
                var donHang = new NhapHangDraw().getByModelView(model.IDNhapHang);

                if (donHang.SoLuongNhap == model.SoluongMoi)
                {
                    if (payment.Equals("1") || payment.Equals("2"))
                    {
                        var result = new NhapHangDraw().UpdateHang(donHang);
                        if (result)
                        {
                            ModelState.AddModelError("nhaphangSuccess", "Nhập Hàng Vào Kho Thành Công");
                            donHang.Status = 2;
                            donHang.StatusInput = true;
                            donHang.StatusPayMent = true;
                            var capnhat = new NhapHangDraw().UpdateStatus(donHang);

                        }
                        else
                        {
                            ModelState.AddModelError("nhaphang", "Lỗi Dữ Liệu");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("nhaphang", "Số lượng nhập hàng không khớp với đơn nhập !!!");
                }
            }
            else
            {
                ModelState.AddModelError("nhaphang", "Lỗi Dữ Liệu !!!");
            }
            return View("Edit");
        }
        [HttpPost]
        public ActionResult LapPhieuNhap(Sanpham model, string soluongNhap)
        {

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(soluongNhap))
                {
                    int soLuong = int.Parse(soluongNhap);
                    if (!string.IsNullOrEmpty(model.IDNCC.ToString()))
                    {
                        var result = new NhapHangDraw().InsertNhapHang(model, soLuong);
                        if (result > 0)
                        {
                            ModelState.AddModelError("nhaphangSuccess", "Tạo đơn nhập thành công");

                        }
                        else
                        {
                            ModelState.AddModelError("nhaphang", "Lỗi không thể tạo đơn nhập !!!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("nhaphang", "Vui lòng chọn nhà cung cấp !!!");
                    }


                }
                else
                {
                    ModelState.AddModelError("nhaphang", "Số lượng nhập không được để trống!!!");
                }
            }
            else
            {
                ModelState.AddModelError("nhaphang", "Lỗi không thể tạo đơn nhập !!!");
            }
            SetViewBagProduct();
            SetViewNhaCungCap();
          
            return View("LapPhieuNhap");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new NhapHangDraw().Delete(id);
            return View();
        }


        [HttpPost]
        public JsonResult DuyetDon(long id)
        {
            var result = new NhapHangDraw().DuyetDon(id);
            return Json(
                new { DuyetDon = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportExel(long id)
        {
            
            var wb = new XLWorkbook(@"D:/Do_An/BaiTapLon/Resource/Template/Nhap_Hang_Template.xlsx");
            var workSheet = wb.Worksheet(1);
            var list = new NhapHangDraw().getById(id);
            workSheet.Cell("E6").Value = list.IDNhapHang;
            workSheet.Cell("E7").Value = list.NgayTao;
            workSheet.Cell("B15").Value = list.NhaCungCap;
            workSheet.Cell("B18").Value = list.DiaChi;
            workSheet.Cell("F15").Value = list.Email;
            workSheet.Cell("D15").Value = "0" + list.SoDienThoai.ToString();



            var listItem = list;
            if (listItem.Status == 0)
            {
                workSheet.Cell("D18").Value = "Chờ Duyệt";

            }
            else
            {

                workSheet.Cell("D18").Value = "Đã Duyệt";
            }


            if (listItem.StatusPayment == true)
            {
                workSheet.Cell("B21").Value = "Tiền mặt";
            }
            else
            {
                workSheet.Cell("B21").Value = "Tiền mặt";
            }

            if (listItem.StatusInput == true)
            {

                workSheet.Cell("F18").Value = "Đã Nhập Kho";
            }
            else
            {
                workSheet.Cell("F18").Value = "Chưa Nhập Kho";
            }

            int row = 24;


            workSheet.Cell("B" + row).Value = list.Name;
            workSheet.Cell("C" + row).Value = list.SoluongMoi;
            workSheet.Cell("D" + row).Value = list.GiaNhap;
            workSheet.Cell("F" + row).Value = list.ThanhTien;


            string nameFile = "Export_Phieu_Nhap_Hang_" + list.IDNhapHang + "_" + DateTime.Now.Ticks + ".xlsx";
            string pathFile = Server.MapPath("~/Resource/ExportFile/" + nameFile);
            try
            {
                wb.SaveAs(pathFile);
            }
            catch
            {
                ViewBag.Error("ÉO LƯU DCD");
            }
            return Json(nameFile, JsonRequestBehavior.AllowGet);
        }

        public void SetViewBagProduct(long? selectedId = null)
        {
            
            var dao = new SanphamDraw();
            ViewBag.CategoryID = new SelectList(dao.danhSachSanPhamTon(), "IDContent", "Name", selectedId);
        }

        public void SetViewNhaCungCap(long? selectedId = null)
        {
            
            var dao = new NhaCungCapDraw();
            ViewBag.IDNCC = new SelectList(dao.listALL(), "IDNCC", "TenNCC", selectedId);
        }
    }
}