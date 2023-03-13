using Mood.EF2;

using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using System.Text;
using System.Threading.Tasks;

namespace Mood.Draw
{
    public class Feed_BackDraw
    {
        QuanLySachDBContext db = null;
        public Feed_BackDraw()
        {
            db = new QuanLySachDBContext();
        }
        public int coutFeedBack()
        {
            return db.Feedbacks.Count();
        }
        public List<Feed_Back> listAll()
        {
            return db.Feedbacks.ToList();
        }
        public IEnumerable<Feed_Back> listAllFeedBack(string searching,int page,int pagesize)
        {
            IEnumerable<Feed_Back> model = db.Feedbacks;
            if(!string.IsNullOrEmpty(searching))
            {
                model = model.Where(x => x.Name.Contains(searching) || x.Address.Contains(searching));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
        }
        public bool RepLyFeedBack(Feed_Back modelFeedBack)
        {
            try
            {
                var feedBack = db.Feedbacks.Find(modelFeedBack.IDFeedBack);
                feedBack.Reply = modelFeedBack.Reply;
                feedBack.Status = true;
                feedBack.UpdateBy = modelFeedBack.UpdateBy;
                feedBack.UpdateDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public Feed_Back viewDetails(long id)
        {
            return db.Feedbacks.Find(id);
        }
        public bool Delete(long id)
        {
            try
            {
                var feedBack = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(feedBack);
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
