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

    [Table("Orders")]
    public partial class Orders
    {
        [Key]
        public long IDOder { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public long? CustomerID { get; set; }

        [Display(Name = "Tên Người Nhận")]
        public string ShipName { get; set; }

        [Display(Name = "Số Điện thoại")]
        public string ShipMobile { get; set; }


        [Display(Name = "Địa Chỉ Nhận")]
        public string ShipAddress { get; set; }

        [Display(Name = "Email")]
        public string ShipEmail { get; set; }

        [Display(Name = "Trạng Thái")]
        public int? Status { get; set; }

        [Display(Name = "Giao Hàng")]
        public int? GiaoHang { get; set; }

        [Display(Name = "Nhận hàng")]
        public int? NhanHang { get; set; }

        [Display(Name = "Ghi Chú")]
        public string ghiChu { get; set; }

        [Display(Name ="Phương Thức Thanh Toán")]
        public string DeliveryPaymentMethod { get; set; }

        [Display(Name = "Thanh Toán")]
        public int StatusPayment { get; set; }

        [Display(Name = "Phương Thức Thanh Toán")]
        public string OrderCode { get; set; }
    }
}
