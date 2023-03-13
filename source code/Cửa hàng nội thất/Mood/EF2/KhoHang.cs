using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.EF2
{
    [Table("KhoHang")]
    public class KhoHang
    {
        [Key]
        public long ID { get; set; }

        public string TenKho { set; get; }
        public long? IDProduct { get; set; }

        public int SoLuongKho { get; set; }
        public int TonKho { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }
        
        public DateTime? NgayTao { get; set; }
    }
}
