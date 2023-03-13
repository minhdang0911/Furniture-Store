using ClosedXML.Excel;
using Mood.Draw;
using Mood.ThongKeModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapLon.Areas.Admin.Controllers
{
    public class ThongKeController : BaseController
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThongKeSanPhamHot()
        {
            ViewBag.listSanPhamHot = new SanphamDraw().listTopSellings(5);
            return View();
        }
        public ActionResult DoanhThu(string fromDate, string todate, int page = 1, int pageSize = 20)
        {
            var thongkeList = new OrderDraw().getDoanhThu(fromDate, todate, page, pageSize);

            int tongThu = 0;
            int tongLai = 0;
            foreach (var item in thongkeList)
            {
                tongThu += (int)item.DoanhThu;
                tongLai += (int)item.TongLai;

            }
            ViewBag.tongThu = tongThu;
            ViewBag.tongLai = tongLai;
            ViewBag.fromDate = fromDate;
            ViewBag.todate = todate;
            return View(thongkeList);
        }
        public ActionResult DoanhThuChart(string fromDate, string todate)
        {
            var thongkeList = new OrderDraw().getDoanhThuChart(fromDate, todate);

            List<int> listDoanhThu = new List<int>();
            List<int> listTongLai = new List<int>();
            List<string> doanhsoNgay = new List<string>();
            int tongThu = 0;
            int tongLai = 0;
            foreach (var item in thongkeList)
            {
                tongThu += (int)item.DoanhThu;
                tongLai += (int)item.TongLai;
                listDoanhThu.Add((int)item.DoanhThu);
                listTongLai.Add((int)item.TongLai);
                doanhsoNgay.Add(item.DoanhThuNgay.ToString("dd/MM/yyyy"));
            }
            ViewBag.listDoanhThu = listDoanhThu;
            ViewBag.listTongLai = listTongLai;
            ViewBag.doanhsoNgay = doanhsoNgay;
            ViewBag.tongThu = tongThu;
            ViewBag.tongLai = tongLai;
            ViewBag.fromDate = fromDate;
            ViewBag.todate = todate;
            return View();
        }
        public ActionResult ExportExel(string fromDate, string toDate)
        {
            
            var wb = new XLWorkbook(@"D:\Do_An\BaiTapLon\Resource\Template/Tempalet_Doanh_Thu.xlsx");
            var workSheet = wb.Worksheet(1);
            if (!string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
            {
                workSheet.Cell("C1").Value = "Thông Kê Doanh Thu";
            }
            else
            {
                workSheet.Cell("C1").Value = "Thông Kê Doanh Thu Cả Năm";
            }

            var list = new OrderDraw().getDoanhThuChart(fromDate, toDate);

            workSheet.Cell("D6").Value = DateTime.Now;


            workSheet.Cell("B16").Value = fromDate;


            workSheet.Cell("D16").Value = toDate;

            int row = 19;

            foreach (var item in list)
            {

                workSheet.Cell("B" + row).Value = item.DoanhThuNgay;
                workSheet.Cell("C" + row).Value = item.DoanhThu;
                workSheet.Cell("D" + row).Value = item.TongLai;
                row++;
            }
            string nameFile = "";
            if (!string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
            {
                nameFile = "Export_Doanh_Thu_Tu_" + fromDate + "_" + toDate + "_" + DateTime.Now.Ticks + ".xlsx";
            }
            else
            {
                nameFile = "Export_Doanh_Thu_Ca_Nam" + "_" + DateTime.Now.Ticks + ".xlsx";
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
    }
}