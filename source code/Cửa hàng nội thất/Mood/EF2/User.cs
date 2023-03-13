namespace Mood.EF2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDUser { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Mật khẩu")]
        public string PassWord { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Không quá 250 kí tự")]
        [Display(Name = "Địa chỉ")]
        public string Adress { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }


        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        [Display(Name = "Chức danh")]
        public long IDQuyen { get; set; }
    }
}
