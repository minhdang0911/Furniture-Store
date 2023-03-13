using Mood.EF2;
using Mood.HoaDonModel;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.Draw
{
    public class Order_DetailDraw
    {
        QuanLySachDBContext db = null;
        public Order_DetailDraw()
        {
            db = new QuanLySachDBContext();
        }
        public bool Insert(Order_Detail oderDetail)
        {
            
            try
            {
                db.Oder_Details.Add(oderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Order_Detail getOrderByID(long id)
        {
            return db.Oder_Details.Find(id);
        }
        public IEnumerable<ChiTietHoaDon> chiTietHoaDon(long id, string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where b.OderID == id && c.Name.Contains(searhString) || b.OderID == id && a.ShipName.Contains(searhString)
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              TacGia = c.TacGia,
                              Price = b.Price,
                              PriceSale = c.PriceSale,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang,
                              NhanHang = a.NhanHang,
                              DeliveryPaymentMethod = a.DeliveryPaymentMethod,
                              StatusPayment = a.StatusPayment
                          }
                             ).OrderByDescending(x => x.Quanlity).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where b.OderID == id
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              TacGia = c.TacGia,
                              Price = b.Price,
                              PriceSale = c.PriceSale,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang,
                              NhanHang = a.NhanHang,
                              DeliveryPaymentMethod = a.DeliveryPaymentMethod,
                              StatusPayment = a.StatusPayment
                          }
                             ).OrderByDescending(x => x.Quanlity).ToPagedList(page, pagesize);
            }
            return result;
        }
        public IEnumerable<ChiTietHoaDon> chiTietHoaDonUser(long id, int page, int pagesize)
        {
            dynamic result;
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where b.OderID == id
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              TacGia = c.TacGia,
                              Price = b.Price,
                              PriceSale = c.PriceSale,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang,
                              NhanHang = a.NhanHang,
                              DeliveryPaymentMethod = a.DeliveryPaymentMethod,
                              StatusPayment = a.StatusPayment
                          }
                             ).OrderByDescending(x => x.Quanlity).ToPagedList(page, pagesize);
            return result;
        }

        public List<ChiTietHoaDon> dataExport(long id, string searhString)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where b.OderID == id && c.Name.Contains(searhString) || b.OderID == id && a.ShipName.Contains(searhString)
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              TacGia = c.TacGia,
                              Price = b.Price,
                              PriceSale = c.PriceSale,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang,
                              NhanHang = a.NhanHang,
                              DeliveryPaymentMethod = a.DeliveryPaymentMethod,
                              StatusPayment = a.StatusPayment
                          }
                             ).OrderByDescending(x => x.Quanlity).ToList();
            }
            else
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where b.OderID == id
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              TacGia = c.TacGia,
                              Price = b.Price,
                              PriceSale = c.PriceSale,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang,
                              NhanHang = a.NhanHang,
                              DeliveryPaymentMethod = a.DeliveryPaymentMethod,
                              StatusPayment = a.StatusPayment
                          }
                             ).OrderByDescending(x => x.Quanlity).ToList();
            }
            return result;
        }

    }
}
