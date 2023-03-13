using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaiTapLon.Models
{
    public class NewPassWordModel
    {
        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Độ dài mật khẩu ít nhất 8 kí tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string NewPassWord { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("PassWord", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPass { set; get; }

        
    }
}