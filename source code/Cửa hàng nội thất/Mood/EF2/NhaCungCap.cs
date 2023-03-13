using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCungCap")]
    public partial class NhaCungCap
    {
        [Key]
        public long IDNCC{ get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên nhà cung cấp")]
        public string TenNCC { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Người Tạo")]
        public long? IDNguoiTao { get; set; }
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
