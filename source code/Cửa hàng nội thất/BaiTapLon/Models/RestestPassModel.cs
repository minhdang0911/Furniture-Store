using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTapLon.Models
{
    public class RestestPassModel
    {
        public long IDUser { set; get; }
        public string oldPassWord { set; get; }
        public string newPassWorrd { set; get; }
        public string confinrmPass { set; get; }
    }
}