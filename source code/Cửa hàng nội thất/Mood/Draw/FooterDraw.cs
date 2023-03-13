using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.Draw
{
    public class FooterDraw
    {
        QuanLySachDBContext db = null;
        public FooterDraw()
        {
            db = new QuanLySachDBContext();
        }
        public Footer getFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }
    }
}
