namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [Key]
        public long IDMenu { get; set; }

        [Display(Name = "Tên Menu")]
        [Required]
        [StringLength(50)]
        public string NameMenu { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        [Display(Name = "Thứ Tự")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "Target")]
        public string Target { get; set; }

        [Display(Name = "Cấp Menu")]
        public long? MenuTypeID { get; set; }


        public DateTime? CreateDate { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }
    }
}
