namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feed_Back")]
    public partial class Feed_Back
    {
        [Key]
        public long IDFeedBack { get; set; }

        [Display(Name = "Tên Khách Hàng")]
        public string Name { get; set; }

        [Display(Name = "Số Điện Thoại")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [Display(Name = "Nội Dung")]
        public string Content { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }
        [Display(Name = "Trả Lời")]
        public string Reply { get; set; }

        [Display(Name = "Tiêu Đề")]
        public string TieuDe { get; set; }

        [Display(Name = "Người Cập Nhật")]
        public string UpdateBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdateDate { get; set; }

        public long? CustomerID { get; set; }

    }
}
