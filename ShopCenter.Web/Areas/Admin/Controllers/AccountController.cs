using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopCenter.Web.Areas.Admin.Models;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ViewResult Login(string ReturnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User != null)
                {
                    await _signInManager.SignOutAsync();
                    var SignInResult = await _signInManager.PasswordSignInAsync(User, model.PassWord, false, false);
                    if (SignInResult.Succeeded)
                    {
                        return Redirect(string.IsNullOrEmpty(model.ReturnUrl) ? "/" : model.ReturnUrl);
                    }
                    else
                    {
                        ModelState.TryAddModelError("", "پسوورد یا نام کاربری اشتباه است");
                    }
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public ViewResult Register( string ReturnUrl = "/")
        {
            ReturnUrl = ReturnUrl ?? "/";
            RegisterViewModel model = new RegisterViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = model.Username,
                    UserName = model.Username
                };
                var Result = await _userManager.CreateAsync(user, model.Password);
                if (Result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    foreach (var item in Result.Errors)
                    {
                        ModelState.TryAddModelError(item.Code, item.Description);
                    }
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> LogOut(string ReturnUrl="/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }
        [AllowAnonymous]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return Redirect(ReturnUrl);
        }
    }
}