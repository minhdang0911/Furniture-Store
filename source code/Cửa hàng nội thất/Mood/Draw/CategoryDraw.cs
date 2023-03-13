using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mood.EF2;
using X.PagedList;

namespace Mood.Draw
{
    public class CategoryDraw
    {
        QuanLySachDBContext db = null;
        public CategoryDraw()
        {
            db = new QuanLySachDBContext();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public List<Category> ListAllCategory(int top)
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x=>x.DisPlayOrder).Take(top).ToList();
        }
        public IEnumerable<Category> ListAll(string searhString,int page, int pagesize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searhString))
            {
                model = model.Where(x => x.TenTheloai.Contains(searhString) || x.NguoiTao.Contains(searhString));
                //Contains tìm chuỗi gần đúng
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pagesize);
        }
        public bool CheckTheloai(string Theloai)
        {
            var result = db.Categories.Count(x => x.TenTheloai == Theloai);
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public long Insert(Category model)
        {
            db.Categories.Add(model);
            db.SaveChanges();
            return model.IDCategory;
        }
        public Category getByID(long id)
        {
            return db.Categories.Find(id);
        }
        public Category ViewDetail(long id)
        {
            return db.Categories.Find(id);
        }
        public bool Update(Category model)
        {
            try
            {
                var category = db.Categories.Find(model.IDCategory);
                category.TenTheloai = model.TenTheloai;
                category.MetaTitle = model.MetaTitle;
                category.ModifiedBy = model.ModifiedBy;
                category.ModifiedDate = model.ModifiedDate;
                category.SEOTitle = model.SEOTitle;
                category.NgayTao = DateTime.Now;
                category.Status = model.Status;
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var result = db.Categories.Find(id);
                db.Categories.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch{
                return false;
            }
        }
    }
}
