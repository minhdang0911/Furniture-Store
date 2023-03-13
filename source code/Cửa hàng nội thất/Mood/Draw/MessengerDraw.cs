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
    public class MessengerDraw
    {
        QuanLySachDBContext db = null;
        public MessengerDraw()
        {
            db = new QuanLySachDBContext();
        }
        public bool Insert(Messenger model)
        {
            try
            {
                db.Messengers.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Orders> listTinNhanUser(long id, string searhString, int page, int pageSize)
        {
            IQueryable<Orders> model = db.Oders.Where(x => x.CustomerID == id);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.ShipName.Contains(searhString) || x.ShipAddress.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }

        public IEnumerable<Feed_Back> listTinNhanFeedBack(long id, string searhString, int page, int pageSize)
        {
            IQueryable<Feed_Back> model = db.Feedbacks.Where(x => x.CustomerID == id);
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.Name.Contains(searhString) || x.Address.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);// nhận 2 giá trị page và page size
            // cần sắp sếp theo thứ tự ngày tạo
        }
        public int getTotalReply(long id)
        {
            return db.Feedbacks.Where(x => x.CustomerID == id).Count();
        }

    }
}
