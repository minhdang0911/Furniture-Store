using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaiTapLon.Models
{
    public class LoginModel
    {
        [Display(Name ="Tên Đăng Nhập")]
        [Required(ErrorMessage ="Bạn phải nhập tên đăng nhập!!!")]
        public string UserName{ set; get; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu!!!")]
        public string PassWord { set; get; }
        public string termsCheckbox { set; get; }
    }
}