using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.SanPhamViewModel
{
    public class SanPhamModel
    {
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

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Images { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(50)]
        [Display(Name = "Chất lượng")]
        public string Quanlity { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTao { get; set; }

        [StringLength(50)]
        [Display(Name = "Người Tạo")]
        public string NguoiTao { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [StringLength(50)]
        [Display(Name = "Nhà Cung Cấp")]
        public string NhaCungCap;

        [Display(Name = "Giá Tiền")]
        public int GiaTien { set; get; }

        [Display(Name = "Giá Nhập")]
        public int? GiaNhap { get; set; }

        [Display(Name = "Giá Khuyến Mại")]
        public int? PriceSale { get; set; }

        [Display(Name = "Mô tả")]
        public string Mota { get; set; }

        [Display(Name = "Chi tiết")]
        public string ChiTiet { get; set; }

        public int? TonKho { set; get; }

    }
}
