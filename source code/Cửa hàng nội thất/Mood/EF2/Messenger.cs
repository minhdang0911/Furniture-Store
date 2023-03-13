using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.EF2
{
    [Table("Messenger")]
    public partial class Messenger
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
    }
}
