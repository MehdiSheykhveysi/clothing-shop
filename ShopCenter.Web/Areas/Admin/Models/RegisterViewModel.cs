using System.ComponentModel.DataAnnotations;

namespace ShopCenter.Web.Areas.Admin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفاایمیل را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Username { get; set; }

        [Required(ErrorMessage = "لطفا پسوورد را وارد کنید")]
        [Display(Name = "پسوورد")]
        [UIHint("Password")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [Display(Name = "تکرار پسوورد")]
        [UIHint("Password")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}
