namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quyen")]
    public partial class Quyen
    {
        [Key]
        public long IDQuyen { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên Quyền")]
        public string TenQuyen { get; set; }

        public bool Status { get; set; }
    }
}
