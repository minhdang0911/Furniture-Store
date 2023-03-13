using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.UserModel
{
    public class UserModelView
    {
        public long IDUser { get; set; }


        [StringLength(50)]

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        public string PassWord { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Không quá 250 kí tự")]
        [Display(Name = "Địa chỉ")]
        public string Adress { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        public long? IDQuyen { get; set; }
        [Display(Name = "Quyền")]
        public string TenQuyen { get; set; }
    }
}
