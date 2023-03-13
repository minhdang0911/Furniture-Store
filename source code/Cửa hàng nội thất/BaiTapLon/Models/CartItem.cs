using Mood.EF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTapLon.Models
{
    public class CartItem
    {
        public Sanpham Product { set; get; }
        public int Quantity { set; get; }
        public int countCart { get; set; }
    }
}