using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BotDetect.Web.Mvc;

namespace BaiTapLon.Models
{
    public class RegisterModel
    {
        
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]

        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20,MinimumLength =8,ErrorMessage ="Độ dài mật khẩu ít nhất 8 kí tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string PassWord { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        
        public string ConfirmPass { set; get; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Name { set; get; }

        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Display(Name = "Số điện thoại")]
        public string Phone { set; get; }
        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        public string Email { set; get; }
        [Display(Name = "Chức danh")]
        public long IDQuyen { set; get; }

    }
}