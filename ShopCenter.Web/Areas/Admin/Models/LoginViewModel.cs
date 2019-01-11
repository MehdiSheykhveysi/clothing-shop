using System.ComponentModel.DataAnnotations;

namespace ShopCenter.Web.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا پسوورد را وارد کنید")]
        [Display(Name = "پسوورد")]
        [UIHint("Password")]
        public string PassWord { get; set; }
        public string ReturnUrl { get; set; }

    }
}
