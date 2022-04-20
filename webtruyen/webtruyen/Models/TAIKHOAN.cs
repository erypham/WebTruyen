namespace webtruyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOAN")]
    public partial class TAIKHOAN
    {
        [Key]
        public int MATK { get; set; }

        public int? MALOAITK { get; set; }

        [StringLength(200)]
        public string TEN_ND { get; set; }

        [StringLength(200)]
        public string EMAIL { get; set; }

        [StringLength(10)]
        public string SDT_ND { get; set; }

        [StringLength(2048)]
        public string PASSWORD_USER { get; set; }

        [StringLength(6)]
        public string OTB { get; set; }

        public virtual LOAITAIKHOAN LOAITAIKHOAN { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(30)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "newPassword")]
        public string newPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirm New password")]
        [Compare("newPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmNewPassword { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [Display(Name = "OTB")]
        public string OTB { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "newPassword")]
        public string newPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirm New password")]
        [Compare("newPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmNewPassword { get; set; }
    }

    public class BigViewModel
    {
        public TAIKHOAN TAIKHOAN { get; set; }
        public ForgotPasswordModel ForgotPasswordModel { get; set; }
    }
}
