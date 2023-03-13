using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mood.NhaCungCapModel
{
    public class NhaCungCapModelView
    {
        public long IDNCC { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên nhà cung cấp")]
        public string TenNCC { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày Tạo")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Người Tạo")]
        public string NguoiTao { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [StringLength(250)]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
