using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Mood.NhaCungCapModel;
using System.Text.RegularExpressions;

namespace Mood.Draw
{
   public class NhaCungCapDraw
    {
        QuanLySachDBContext db = null;
        public const string EMAIL_PATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string EMAIL_PATTERN_IN_TEXT = @"[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        public NhaCungCapDraw()
        {
            db = new QuanLySachDBContext();
        }
        public List<NhaCungCap> listALL()
        {
            return db.NhaCungCaps.Where(x => x.Status == true).ToList();
        }
        public List<User> listALLUserAdmin()
        {
            return db.Users.Where(x => x.Status == true && x.IDQuyen == 1).ToList();
        }
        public IEnumerable<NhaCungCapModelView> listALL(string searchString,int page,int pagesize)
        {
            dynamic result;
            if(!string.IsNullOrEmpty(searchString))
            {
                result = (from b in db.NhaCungCaps
                          join c in db.Users on b.IDNguoiTao equals c.IDUser
                          where b.TenNCC.Contains(searchString) || c.UserName.Contains(searchString) || b.DiaChi.Contains(searchString)
                          select new NhaCungCapModelView()
                          {
                              IDNCC = b.IDNCC,
                              TenNCC = b.TenNCC,
                              NguoiTao = c.UserName,
                              NgayTao = b.NgayTao,
                              Status = b.Status,
                              DiaChi = b.DiaChi,
                              SoDienThoai = b.SoDienThoai,
                              Email = b.Email
                          }).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }else
            {
                result = (from b in db.NhaCungCaps
                          join c in db.Users on b.IDNguoiTao equals c.IDUser
                          
                          select new NhaCungCapModelView()
                          {
                              IDNCC = b.IDNCC,
                              TenNCC = b.TenNCC,
                              NguoiTao = c.UserName,
                              NgayTao = b.NgayTao,
                              Status = b.Status,
                              DiaChi = b.DiaChi,
                              SoDienThoai = b.SoDienThoai,
                              Email = b.Email
                          }).OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
            }
            return result;
        }
        public bool CheckNCC(string tenNCC)
        {
            var result = db.NhaCungCaps.Count(x => x.TenNCC == tenNCC);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public long Insert(NhaCungCap model)
        {
            db.NhaCungCaps.Add(model);
            db.SaveChanges();
            return model.IDNCC;
        }
        public bool IsValidEmail(string strIn)
        {
            bool isEmail = false;
            try
            {
                Regex reg = new Regex(EMAIL_PATTERN);
                isEmail = reg.IsMatch(strIn);
            }
            catch
            {

            }
            return isEmail;
        }
        public NhaCungCap getByID(long idNhaCungCap)
        {
            return db.NhaCungCaps.Find(idNhaCungCap);
        }
        public bool CheckSDT(string SDT)
        {
            Regex check = new Regex(@"^[0-9]+$");
            return check.IsMatch(SDT);
        }
        public bool Delete(long id)
        {
            try
            {
                var result = db.NhaCungCaps.Find(id);
                db.NhaCungCaps.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(NhaCungCap model)
        {
            try
            {
                var nhacungcap = db.NhaCungCaps.Find(model.IDNCC);
                nhacungcap.TenNCC = model.TenNCC;
                nhacungcap.IDNguoiTao = model.IDNguoiTao;
                nhacungcap.DiaChi = model.DiaChi;
                nhacungcap.Email = model.Email;
                nhacungcap.SoDienThoai = model.SoDienThoai;
                nhacungcap.NgayTao = DateTime.Now;
                nhacungcap.Status = model.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
