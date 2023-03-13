using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.EF2
{
    [Table("NhapHang")]
    public partial class NhapHang
    {
        [Key]
        public long IDNhapHang { get; set; }


        public long? IDSanPham { get; set; }

        public int? SoLuongNhap { get; set; }

        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }

        public DateTime?NgayTao { get; set; }

        public long? IDNCC { get; set; }

        public long? IDCategory { get; set; }

        public long? IDNguoiTao { get; set; }

        public int? GiaNhap { set; get; }

        public bool? StatusPayMent { set; get; }
        public bool? StatusInput { set; get; }
    }
}
