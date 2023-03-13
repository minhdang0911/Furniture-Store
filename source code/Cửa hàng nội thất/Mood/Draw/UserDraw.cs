using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mood.EF2;
using Mood.UserModel;
using System.Text.RegularExpressions;
using X.PagedList;
using Mood.HoaDonModel;

namespace Mood.Draw
{
    public class UserDraw
    {
        QuanLySachDBContext db = null;
        public const string EMAIL_PATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string EMAIL_PATTERN_IN_TEXT = @"[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        public UserDraw()
        {
            db = new QuanLySachDBContext();
        }
        public int coutUser()
        {
            return db.Users.Count();
        }
        public IEnumerable<User> ListUserALL(string searhString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.UserName.Contains(searhString) || x.Name.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }
        public List<Quyen> ListUserNomarl()
        {
            return db.Quyens.Where(x =>x.Status == true && x.IDQuyen !=1).ToList();
        }
        public List<Quyen> ListUserAdmin()
        {
            return db.Quyens.Where(x => x.Status == true && x.IDQuyen == 1).ToList();
        }
        public User getIDByUser(long id)
        {
            return db.Users.Find(id);
        }
        public IEnumerable<UserModelView> ListViewUser(string searhString, int page, int pageSize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.Users
                          join b in db.Quyens on a.IDQuyen equals b.IDQuyen
                          where a.Name.Contains(searhString) || b.TenQuyen.Contains(searhString) || a.UserName.Contains(searhString) || a.Adress.Contains(searhString)
                          select new UserModelView()
                          {
                              IDUser = a.IDUser,
                              UserName = a.UserName,
                              PassWord = a.PassWord,
                              Name = a.Name,
                              Adress = a.Adress,
                              Email = a.Email,
                              Phone = a.Phone,
                              Status = a.Status,
                              NgayTao = a.NgayTao,
                              NguoiTao = a.NguoiTao,
                              ModifiedBy = a.ModifiedBy,
                              ModifiedDate = a.ModifiedDate,
                              IDQuyen = a.IDQuyen,
                              TenQuyen = b.TenQuyen,
                          }).OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
            }
            else
            {
                result = (from a in db.Users
                          join b in db.Quyens on a.IDQuyen equals b.IDQuyen
                          select new UserModelView()
                          {
                              IDUser = a.IDUser,
                              UserName = a.UserName,
                              PassWord = a.PassWord,
                              Name = a.Name,
                              Adress = a.Adress,
                              Email = a.Email,
                              Phone = a.Phone,
                              Status = a.Status,
                              NgayTao = a.NgayTao,
                              NguoiTao = a.NguoiTao,
                              ModifiedBy = a.ModifiedBy,
                              ModifiedDate = a.ModifiedDate,
                              IDQuyen = a.IDQuyen,
                              TenQuyen = b.TenQuyen,
                          }).OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
            }
            return result;
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges(); 
            return entity.IDUser;
        }
        public long InsertFaceBook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if(user == null)
            {
                entity.NgayTao = DateTime.Now;
                entity.IDQuyen = 2;
                entity.Status = true;
                entity.PassWord = "827ccb0eea8a706c4c34a16891f84e7b";
                entity.Adress = "Chưa có thông tin";
                entity.Phone = "Chưa có thông tin";
                db.Users.Add(entity);
                
                db.SaveChanges();
                return entity.IDUser;
            }
            else
            {
                return user.IDUser;
            }
        }
        public bool CheckSDT(string SDT)
        {
            Regex check = new Regex(@"^[0-9]+$");
            return check.IsMatch(SDT);
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
        public bool checkMail(string mail)
        {
            var result = db.Users.Count(x => x.Email == mail);
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkMailUser(string mail)
        {
            var result = db.Users.Count(x => x.Email == mail && x.IDQuyen != 1);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkMailUserRetest(string mail,string user)
        {
            var result = db.Users.Count(x => x.Email == mail && x.IDQuyen != 1 && x.UserName == user);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public User getbyEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email && x.IDQuyen != 1);
        }
        public bool checkUserName(string user)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == user);
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public User getByID(string userName)
        {
            return db.Users.SingleOrDefault(x =>x.UserName == userName);
        }
        public User getByIDLogin(long id)
        {
            return db.Users.Find(id);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public bool Delete(long id)
        {
            try
            {
                var result = db.Users.Find(id);
                db.Users.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool UpdatePass(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.IDUser);
                user.PassWord = entity.PassWord;
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.IDUser);
                user.Name = entity.Name;// lấy Name đc truyền từ VIew
                if (!string.IsNullOrEmpty(entity.PassWord))
                {
                    user.PassWord = entity.PassWord;// đảm bảo nếu người dùng k nhập pass thì nó sẽ k update
                }
                user.Adress = entity.Adress;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.IDQuyen = entity.IDQuyen;
                user.ModifiedBy = entity.ModifiedBy;
                user.Status = entity.Status;
                user.NgayTao = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePassword(User entity,string passWordNew)
        {
            try
            {
                var user = db.Users.Find(entity.IDUser);
                if (!string.IsNullOrEmpty(entity.PassWord))
                {
                    user.PassWord = entity.PassWord;// đảm bảo nếu người dùng k nhập pass thì nó sẽ k update
                    user.PassWord = passWordNew;
                }
                
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public bool UpdateUser(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.IDUser);
                user.Name = entity.Name;// lấy Name đc truyền từ VIew
                user.Adress = entity.Adress;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.IDQuyen = entity.IDQuyen;
                user.ModifiedBy = entity.ModifiedBy;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<Orders> ListALLHoaDonUSer(long id,string searhString, int page, int pageSize)
        {
            IQueryable<Orders> model = db.Oders.Where(x=>x.CustomerID == id && x.NhanHang != 1);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.ShipName.Contains(searhString) || x.ShipAddress.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }
        public IEnumerable<ChiTietHoaDon> chiTietHoaDonUser(long id, string searhString, int page, int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.Oders
                          join b in db.Oder_Details on a.IDOder equals b.OderID
                          join c in db.Sanphams on b.ProductID equals c.IDContent
                          where b.OderID == id  && c.Name.Contains(searhString) || b.OderID == id && a.ShipName.Contains(searhString)
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
                              Price = b.Price,
                              NgayTao = a.NgayTao,
                              Status = a.Status,
                              GiaoHang = a.GiaoHang
                          }
                             ).OrderByDescending(x => x.Quanlity).ToPagedList(page, pagesize);
            }
            return result;
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if(result == null)
            {
                return 0;//Tài khoản không tồn tại
            }
            else
            {
                if(result.IDQuyen == 1)
                {
                    if (result.Status == false)
                    {
                        return -1;//Tài khoản đã bị khóa
                    }
                    else
                    {
                        if (result.PassWord == passWord)
                        {
                            return 1;//Đăng nhâp thành công
                        }
                        else
                        {
                            return -2; //Mật khẩu sai
                        }
                    }
                }
                else
                {
                    return -3;//Tài khoản ko có quyền truy cập
                }
            }
        }

        public int LoginHomeUser(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;//Tài khoản không tồn tại
            }
            else
            {
                if (result.IDQuyen != 1)
                {
                    if (result.Status == false)
                    {
                        return -1;//Tài khoản đã bị khóa
                    }
                    else
                    {
                        if (result.PassWord == passWord)
                        {
                            return 1;//Đăng nhâp thành công
                        }
                        else
                        {
                            return -2; //Mật khẩu sai
                        }
                    }
                }
                else
                {
                    return -3;//Tài khoản ko có quyền truy cập
                }
            }
        }
    }
}
