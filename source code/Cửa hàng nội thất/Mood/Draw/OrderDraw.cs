using Mood.EF2;
using Mood.HoaDonModel;
using Mood.ThongKeModel;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.Draw
{
    public class OrderDraw
    {
        QuanLySachDBContext db = null;

        public OrderDraw()
        {
            db = new QuanLySachDBContext();
        }
        public int coutOrder()
        {
            return db.Oders.Count();
        }
        public List<Orders> getListALL()
        {
            return db.Oders.Where(x => x.Status == 1 || x.Status == 0).ToList();
        }

        public int getToTolMes(long id)
        {
            return db.Oders.Where(x => x.CustomerID == id).Count();
        }
        public Orders getOrderByID(long id)
        {
            return db.Oders.Find(id);
        }
        public Orders getOrderByOrderCode(string code)
        {
            return db.Oders.Where(x => x.OrderCode == code).FirstOrDefault();
        }
        public List<Orders> getOrderByIDCustomer(long id)
        {
            return db.Oders.Where(x => x.CustomerID == id).ToList();
        }
        public long Insert(Orders oder)
        {
            db.Oders.Add(oder);
            db.SaveChanges();
            return oder.IDOder;
        }
        public List<Order_Detail> getProductByOrder_Details(long idOrder)
        {
            return db.Oder_Details.Where(x => x.OderID == idOrder).ToList();
        }
        public IEnumerable<Orders> ListALL(string searhString, int page, int pageSize)
        {
            IQueryable<Orders> model = db.Oders.Where(x => x.Status == 0);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.ShipName.Contains(searhString) || x.ShipAddress.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }

        public IEnumerable<Orders> listChoGiao(string searhString, int page, int pageSize)
        {
            IQueryable<Orders> model = db.Oders.Where(x => x.Status == 1 && x.GiaoHang == 0);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.ShipName.Contains(searhString) || x.ShipAddress.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }
        public IEnumerable<Orders> listXuatKho(string searhString, int page, int pageSize)
        {
            IQueryable<Orders> model = db.Oders.Where(x => x.Status == 1 && x.GiaoHang == 1 && x.NhanHang == 0);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.ShipName.Contains(searhString) || x.ShipAddress.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }
        public IEnumerable<Orders> listHoanThanh(string searhString, int page, int pageSize)
        {
            IQueryable<Orders> model = db.Oders.Where(x => x.Status == 1 && x.GiaoHang == 2 && x.NhanHang == 0 || x.NhanHang == 1 || x.NhanHang == 2);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.ShipName.Contains(searhString) || x.ShipAddress.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }

        public IEnumerable<Orders> listTraLai(string searhString, int page, int pageSize)
        {
            IQueryable<Orders> model = db.Oders.Where(x => x.Status == 1 && x.GiaoHang == 1 && x.NhanHang == 2);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.ShipName.Contains(searhString) || x.ShipAddress.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }

        public IEnumerable<ThongKeModelView> getDoanhThu(string fromDate, string toDate, int page, int pageSize)
        {
            var query = from o in db.Oders
                        join od in db.Oder_Details
                        on o.IDOder equals od.OderID
                        join p in db.Sanphams
                        on od.ProductID equals p.IDContent
                        select new
                        {
                            CreatedDate = o.NgayTao,
                            Quantity = od.Quanlity,
                            Price = od.Price,
                            OriginalPrice = p.GiaNhap
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime start = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("vi-VN"));

                query = query.Where(x => x.CreatedDate >= start);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("vi-VN"));

                query = query.Where(x => x.CreatedDate <= endDate);
            }

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate ?? DateTime.Now))
                .Select(r => new
                {
                    Date = r.Key.Value,
                    TotalBuy = r.Sum(x => x.OriginalPrice * x.Quantity),
                    TotalSell = r.Sum(x => x.Price * x.Quantity),
                }).Select(x => new ThongKeModelView()
                {
                    DoanhThuNgay = x.Date,
                    TongLai = x.TotalSell - x.TotalBuy,
                    DoanhThu = x.TotalSell
                });
            return result.OrderByDescending(x => x.DoanhThuNgay).ToPagedList(page, pageSize);
        }
        public IEnumerable<ThongKeModelView> getDoanhThuChart(string fromDate, string toDate)
        {
            var query = from o in db.Oders
                        join od in db.Oder_Details
                        on o.IDOder equals od.OderID
                        join p in db.Sanphams
                        on od.ProductID equals p.IDContent
                        select new
                        {
                            CreatedDate = o.NgayTao,
                            Quantity = od.Quanlity,
                            Price = od.Price,
                            OriginalPrice = p.GiaNhap
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime start = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("vi-VN"));

                query = query.Where(x => x.CreatedDate >= start);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("vi-VN"));

                query = query.Where(x => x.CreatedDate <= endDate);
            }

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate ?? DateTime.Now))
                .Select(r => new
                {
                    Date = r.Key.Value,
                    TotalBuy = r.Sum(x => x.OriginalPrice * x.Quantity),
                    TotalSell = r.Sum(x => x.Price * x.Quantity),
                }).Select(x => new ThongKeModelView()
                {
                    DoanhThuNgay = x.Date,
                    TongLai = x.TotalSell - x.TotalBuy,
                    DoanhThu = x.TotalSell
                });
            return result.ToList();
        }
        public bool Delete(long id)
        {
            try
            {
                var result = db.Oders.Find(id);
                var order_detai = db.Oder_Details.Where(x => x.OderID == id).ToList();
                foreach(var item in order_detai)
                {
                    Delete_Order_Details(item.OderID);
                }
                db.Oders.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete_Order_Details(long id)
        {
            try
            {
                var order_details = db.Oder_Details.Find(id);
                db.Oder_Details.Remove(order_details);
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public int? ChangeStatusOrder(long id)
        {
            var user = db.Oders.Find(id);
            if (user.Status == 1)
            {
                user.Status = 0;
            }
            else
            {
                user.Status = 1;
            }
            /*
            if(user.Status!=2)
            {
                user.Status++;
            }else
            {
                user.Status = 0;
            }
            */
            db.SaveChanges();
            return user.Status;
        }

        public int? ChangeGiaoHang(long id)
        {
            var user = db.Oders.Find(id);
            if (user.GiaoHang == 1)
            {
                user.GiaoHang = 0;
            }
            else
            {
                user.GiaoHang = 1;
            }
            db.SaveChanges();
            return user.GiaoHang;
        }

        public int? ChangeXuatKho(long id)
        {
            var user = db.Oders.Find(id);
            if (user.GiaoHang == 1)
            {
                user.GiaoHang = 2;
                if (user.GiaoHang == 2)
                {
                    var order_detail = new OrderDraw().getProductByOrder_Details(id);
                    foreach (var item in order_detail)
                    {
                        new SanphamDraw().UpdateTonKho(item.ProductID, (int)item.Quanlity);
                    }
                }
                
            }
            else if(user.GiaoHang == 2)
            {
                user.GiaoHang = 1;
                if (user.GiaoHang == 1)
                {
                    var order_detail = new OrderDraw().getProductByOrder_Details(id);
                    foreach (var item in order_detail)
                    {
                        new SanphamDraw().UpdateTraHang(item.ProductID, (int)item.Quanlity);
                    }
                }
            }
            db.SaveChanges();
            return user.GiaoHang;
        }
        public int? ChangeHoanThanh(long id)
        {
            var user = db.Oders.Find(id);
            
            if (user.NhanHang != 2)
            {
                user.NhanHang++;

            }
            else
            {
                
                user.NhanHang = 0;
            }
            
            db.SaveChanges();
            return user.NhanHang;
        }

        public int? ChangeHoanThanhGiao(long id)
        {
            var user = db.Oders.Find(id);
            if (user.GiaoHang != 3)
            {
                user.GiaoHang++;
            }
            else
            {
                user.GiaoHang = 1;
            }
            db.SaveChanges();
            return user.GiaoHang;
        }
        public bool UpdateGhiChu(Orders model)
        {
            try
            {
                var category = db.Oders.Find(model.IDOder);
                category.ghiChu = model.ghiChu;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTrangThaiThanhToan(Orders model)
        {
            try
            {
                var category = db.Oders.Find(model.IDOder);
                category.StatusPayment = model.StatusPayment;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<ChiTietHoaDon> giaoHang(string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where a.Status == 1 && a.GiaoHang == 0 && c.Name.Contains(searhString) || a.Status == 1 && a.GiaoHang == 0 && a.ShipName.Contains(searhString)
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              Price = b.Price,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where a.Status == 1 && a.GiaoHang == 0
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              Price = b.Price,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            return result;
        }

        public IEnumerable<ChiTietHoaDon> hoanThanh(string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where a.Status == 1 && a.GiaoHang == 1 && c.Name.Contains(searhString) || a.Status == 1 && a.GiaoHang == 1 && a.ShipName.Contains(searhString)
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              Price = b.Price,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang,
                              NhanHang = a.NhanHang,
                              ghiChu = a.ghiChu
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where a.Status == 1 && a.GiaoHang == 1
                          select new ChiTietHoaDon()
                          {
                              OderID = a.IDOder,
                              TenKH = a.ShipName,
                              Address = a.ShipAddress,
                              Phone = a.ShipMobile,
                              Email = a.ShipEmail,
                              Quanlity = b.Quanlity,
                              Name = c.Name,
                              Price = b.Price,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang,
                              NhanHang = a.NhanHang,
                              ghiChu = a.ghiChu
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            return result;
        }
    }
}
