using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.HoaDonModel
{
    public class MessengerModel
    {
        [Key]
        public long IDMes { get; set; }

        public long? IDCustomer { get; set; }


        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Nội Dung")]
        public string Content { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public long? IDOrder { set; get; }
        [Display(Name = "Trạng Thái")]
        public int? XacNhan { get; set; }

        [Display(Name = "Giao Hàng")]
        public int? GiaoHang { get; set; }

        [Display(Name = "Nhận hàng")]
        public int? NhanHang { get; set; }

        [Display(Name = "Ghi Chú")]
        public string ghiChu { get; set; }
    }
}

