using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood.MenuModel
{
    public class MeNuModelView
    {
        public long IDMenu { get; set; }

        [Display(Name = "Tên Menu")]
        [Required]
        [StringLength(50)]
        public string NameMenu { get; set; }

        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        public string Target { get; set; }

        public long? MenuTypeID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreateDate { get; set; }
        public string NameType { get; set; }
    }
}
