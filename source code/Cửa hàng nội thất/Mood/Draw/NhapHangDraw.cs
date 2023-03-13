using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mood.NhapHangModel;
using X.PagedList;

namespace Mood.Draw
{
    public class NhapHangDraw
    {
        QuanLySachDBContext db = null;
        public NhapHangDraw()
        {
            db = new QuanLySachDBContext();
        }
        public int coutNhapHang()
        {
            return db.NhapHangs.Count();
        }
        public NhapHang getByModelView(long id)
        {
            return db.NhapHangs.Find(id);
        }
        public NhapHangModelView getById(long id)
        {
            var result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where a.IDNhapHang == id
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              DiaChi = c.DiaChi,
                              Email = c.Email,
                              SoDienThoai = c.SoDienThoai,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              GiaNhap = (int)b.GiaNhap,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString(),

                          }
                              ).SingleOrDefault();
            return result;
        }
        public bool DeleteHang(long id)
        {
            try
            {
                var nhapHang = db.NhapHangs.SingleOrDefault(x => x.IDSanPham == id);
                db.NhapHangs.Remove(nhapHang);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var nhapHang = db.NhapHangs.Find(id);
                db.NhapHangs.Remove(nhapHang);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateStatus(NhapHang model)
        {
            try
            {
                var nhapHang = db.NhapHangs.Find(model.IDNhapHang);
                nhapHang.Status = model.Status;
                nhapHang.StatusInput = model.StatusInput;
                nhapHang.StatusPayMent = model.StatusPayMent;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateHang(NhapHang model)
        {
            try
            {
                var sp = db.Sanphams.Find(model.IDSanPham);
            
                sp.Soluong +=(int)model.SoLuongNhap;
                sp.TonKho += model.SoLuongNhap;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<NhapHangModelView> nhapHangView(string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where b.Name.Contains(searhString) && a.Status == 0 || c.TenNCC.Contains(searhString) && a.Status == 0
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString(),
                              
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where a.Status == 0
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString()
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            return result;
        }

        public IEnumerable<NhapHangModelView> DuyetView(string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where b.Name.Contains(searhString) && a.Status == 0 || a.Status == 1 || c.TenNCC.Contains(searhString) && a.Status == 0 || a.Status ==1
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString(),

                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where a.Status==0 || a.Status == 1
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString()
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            return result;
        }

        public IEnumerable<NhapHangModelView> nhapKhoView(string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where b.Name.Contains(searhString) && a.Status == 1 || c.TenNCC.Contains(searhString) && a.Status == 1
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString(),

                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where a.Status == 1
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString()
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            return result;
        }

        public IEnumerable<NhapHangModelView> nhapHoanThanhView(string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where b.Name.Contains(searhString) && a.Status == 2 || c.TenNCC.Contains(searhString) && a.Status == 2
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString(),

                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.NhapHangs
                          join b in db.Sanphams on a.IDSanPham equals b.IDContent
                          join c in db.NhaCungCaps on a.IDNCC equals c.IDNCC
                          where a.Status == 2
                          select new NhapHangModelView()
                          {
                              IDNhapHang = a.IDNhapHang,
                              IDContent = b.IDContent,
                              Name = b.Name,
                              Metatile = b.MetaTitle,
                              TacGia = b.TacGia,
                              NhaXuatBan = b.NhaXuatBan,
                              Soluong = b.Soluong,
                              NgayTao = b.NgayTao,
                              Status = a.Status,
                              StatusInput = a.StatusInput,
                              StatusPayment = a.StatusPayMent,
                              GiaTien = b.GiaTien,
                              NhaCungCap = c.TenNCC,
                              SoluongMoi = a.SoLuongNhap,
                              ThanhTien = (a.SoLuongNhap * b.GiaNhap).ToString()
                          }
                             ).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            return result;
        }

        
        
        public int? DuyetDon(long id)
        {
            var donHang = db.NhapHangs.Find(id);
            if(donHang.Status != 1)
            {
                donHang.Status++;
            }else {
                donHang.Status=0;
            }
            db.SaveChanges();
            return (donHang.Status);
        }
        public List<NhapHang> getListNhap()
        {
            return db.NhapHangs.ToList();
        }
        public long InsertNhapHang(Sanpham model, int soluongMoi)
        {
            var sanPhamNhap = new NhapHang();
            sanPhamNhap.IDSanPham = model.IDContent;
            sanPhamNhap.IDNCC = model.IDNCC;
            sanPhamNhap.IDCategory = model.CategoryID;
            sanPhamNhap.IDNguoiTao = model.IDNguoiTao;
            sanPhamNhap.NgayTao = DateTime.Now;
            sanPhamNhap.Status = 0;
            sanPhamNhap.GiaNhap = model.GiaNhap;
            sanPhamNhap.StatusPayMent = false;
            sanPhamNhap.StatusInput = false;
            sanPhamNhap.SoLuongNhap = soluongMoi;
            db.NhapHangs.Add(sanPhamNhap);
            db.SaveChanges();
            return sanPhamNhap.IDNhapHang;
            
        }
        
        }
    }

