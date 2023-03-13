using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.NhapHangModel
{
    public class NhapHangModelView
    {
        public long IDNhapHang { get; set; }
        public long IDContent { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên sách")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Metatile { get; set; }

        [StringLength(50)]
        [Display(Name = "Tác giả")]
        public string TacGia { get; set; }

        [StringLength(500)]
        [Display(Name = "Nhà xuất bản")]
        public string NhaXuatBan { get; set; }
        [StringLength(250)]
        [Display(Name = "Tên thể loại")]
        public string TenTheloai { get; set; }

        [Display(Name = "Số lượng")]
        public int Soluong { get; set; }

        [Display(Name = "Số lượng Nhập")]
        public int? SoluongMoi { get; set; }

        [StringLength(250)]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Images { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(50)]
        [Display(Name = "Chất lượng")]
        public string Quanlity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        [Display(Name = "Người Tạo")]
        public string NguoiTao { get; set; }
        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }

        [StringLength(50)]
        [Display(Name = "Nhà Cung Cấp")]
        public string NhaCungCap { set; get; }

        [Display(Name = "Giá Tiền")]
        public int GiaTien { set; get; }
        [Display(Name = "Mô tả")]

        public int GiaNhap { set; get; }
        public string ThanhTien { set; get; }
        public bool? StatusPayment { set; get; }
        public bool? StatusInput { set; get; }
        public string Mota { get; set; }

        [Display(Name = "Chi tiết")]
        public string ChiTiet { get; set; }
    }
}

