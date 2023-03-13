namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        public long IDContact { get; set; }


        [Display(Name = "Nội Dung")]
        public string Content { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}