using Mood.EF2;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.Draw
{
    public class SildeDraw
    {
        QuanLySachDBContext db = null;
        public SildeDraw()
        {
            db = new QuanLySachDBContext();
        }
        public List<Slide> listALLSilde()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisPlayOrder).ToList();
        }
        public bool checkThuTu(int displayOrder)
        {
            var slider = db.Slides.Count(x => x.DisPlayOrder == displayOrder);
            if(slider > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public long InsertSlider(Slide model)
        {
            db.Slides.Add(model);
            db.SaveChanges();
            return model.Id;
        }
        public bool DeleteSlider(long id)
        {
            try
            {
                var slider = db.Slides.Find(id);
                db.Slides.Remove(slider);
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }

        public bool UpdateSilder(Slide modelUpdate)
        {
            try
            {
                var silder = db.Slides.Find(modelUpdate.Id);
                silder.Image = modelUpdate.Image;
                silder.Link = modelUpdate.Link;
                silder.Title = modelUpdate.Title;
                silder.Status = modelUpdate.Status;
                silder.DisPlayOrder = modelUpdate.DisPlayOrder;
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public Slide viewDetails(long id)
        {
            return db.Slides.Find(id);
        }
        public IEnumerable<Slide> listSilderView(string searching, int page, int pagesize)
        {
            IQueryable<Slide> model = db.Slides.Where(x => x.Status == true);
            if (!string.IsNullOrEmpty(searching))
            {
                model = model.Where(x => x.Status == true);
            }
            return model.OrderByDescending(x => x.DisPlayOrder).ToPagedList(page, pagesize);

        }
    }
}
