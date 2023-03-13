using Mood.EF2;
using Mood.MenuModel;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.Draw
{
    public class MenuDraw
    {
        QuanLySachDBContext db = null;
        public MenuDraw()
        {
            db = new QuanLySachDBContext();
        }
        public List<Menu> ListAllByID(int groupID)
        {
            return db.Menus.Where(x => x.MenuTypeID == groupID && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public List<MenuType> listAllMenuType()
        {
            return db.MenuType.ToList();
        }
        public Menu viewDetails(long id)
        {
            return db.Menus.Find(id);
        }
        public bool checkNameMenu(string nameMenu)
        {
            var result = db.Menus.Count(x => x.NameMenu == nameMenu);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<Menu> listAll()
        {
            return db.Menus.Where(x => x.Status == true && x.DisplayOrder != 1 && x.MenuTypeID == 1).OrderByDescending(x => x.DisplayOrder).ToList();
        }
        public bool Delete(long id)
        {
            try
            {
                var menuModel = db.Menus.Find(id);
                db.Menus.Remove(menuModel);
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public long InsertMenu(Menu modelMenu)
        {
            db.Menus.Add(modelMenu);
            db.SaveChanges();
            return modelMenu.IDMenu;
        }
        public bool UpdateMenu(Menu modelMenu)
        {
            try
            {
                var menu = db.Menus.Find(modelMenu.IDMenu);
                menu.NameMenu = modelMenu.NameMenu;
                menu.Link = modelMenu.Link;
                menu.DisplayOrder = modelMenu.DisplayOrder;
                menu.Target = modelMenu.Target;
                menu.MenuTypeID = modelMenu.MenuTypeID;
                menu.Status = modelMenu.Status;
                menu.CreateDate = modelMenu.CreateDate;
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public IEnumerable<MeNuModelView> listMenu(string searhString, int page,int pagesize)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(searhString))
            {
                result = (from a in db.MenuType
                          join b in db.Menus on a.MenuTypeID equals b.MenuTypeID 
                          where b.NameMenu.Equals(searhString) || a.NameType.Equals(searhString)
                          select new MeNuModelView()
                          {
                              IDMenu = b.IDMenu,
                              NameMenu = b.NameMenu,
                              Link = b.Link,
                              DisplayOrder = b.DisplayOrder,
                              Target = b.Target,
                              CreateDate = b.CreateDate,
                              MenuTypeID = b.MenuTypeID,
                              NameType = a.NameType,
                              Status = b.Status
                          }
                             ).OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
            }
            else
            {
                result = (from a in db.MenuType
                          join b in db.Menus on a.MenuTypeID equals b.MenuTypeID
                          select new MeNuModelView()
                          {
                              IDMenu = b.IDMenu,
                              NameMenu = b.NameMenu,
                              Link = b.Link,
                              DisplayOrder = b.DisplayOrder,
                              Target = b.Target,
                              MenuTypeID = b.MenuTypeID,
                              NameType = a.NameType,
                              CreateDate = b.CreateDate,
                              Status = b.Status
                          }
                             ).OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
            }
            return result;
        }
    }
}
