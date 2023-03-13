using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.HoaDonModel
{
    public class ChiTietHoaDon
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public long ProductID { get; set; }

        [Display(Name = "Tên sách")]
        public string Name { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string TenKH { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Tác Giả")]
        public string TacGia { get; set; }

        [Display(Name ="Mã hóa đơn")]
        public long OderID { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quanlity { get; set; }

        [Display(Name = "NgayTao")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Giá")]
        public int? Price { get; set; }

        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }
        [Display(Name = "Giao Hàng")]
        public int? GiaoHang { get; set; }

        [Display(Name = "Nhận hàng")]
        public int? NhanHang { get; set; }

        [Display(Name = "Ghi Chú")]
        public string ghiChu { get; set; }

        [Display(Name = "Phương Thức Thanh Toán")]
        public string DeliveryPaymentMethod { get; set; }

        [Display(Name = "Thanh Toán")]
        public int StatusPayment { get; set; }
        [Display(Name = "Giá Khuyến Mại")]
        public int? PriceSale { get; set; }

    }
}
