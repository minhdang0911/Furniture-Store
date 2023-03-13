using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTapLon.Common
{
    public class UserLogin
    {
        public long userId { set; get; }
        public string userName { set; get; }
        
        public string phone { set; get; }
        public string email { set; get; }
        public string address { set; get; }
        public string name { set; get; }

        public int tolMes { set; get; }
        public int tolRep { set; get; }

        public int totalMessenger { set; get; }
    }
}