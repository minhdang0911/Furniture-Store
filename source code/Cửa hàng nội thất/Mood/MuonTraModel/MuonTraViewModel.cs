using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.MuonTraModel
{
    public class MuonTraViewModel
    {
        public long IdMuon { get; set; }
        [Display(Name = "Tên sách")]
        public long IDContent { get; set; }
        [Display(Name = "Tên người mượn")]
        public long IDUser { get; set; }

        [Display(Name = "Số lượng mượn")]
        public int SoluongMuon { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ tên")]
        public string Hoten { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên sách")]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Thời gian mượn")]
        public DateTime? ThoigianM { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Thời gian hẹn trả")]
        public DateTime? ThoiGianHenTra { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Thời gian trả")]
        public DateTime? ThoigianTra { get; set; }
        [Display(Name = "Lần gia hạn")]
        public int? SoLanGiaHan { get; set; }
    }
}
