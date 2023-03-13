namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [Key]
        public long IDCategory { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên thể loại")]
        public string TenTheloai { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ tiêu đề")]
        public string MetaTitle { get; set; }

        public long? ParentID { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ SEO")]
        public string SEOTitle { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [StringLength(250)]
        public string NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người tạo")]
        public string ModifiedBy { get; set; }

        public int? DisPlayOrder { get; set; }

        public string MetaDescriptions { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
