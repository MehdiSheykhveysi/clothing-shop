using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Dal.EF;
using Shop.Domain.Entities;
using ShopCenter.Web.Models;

namespace ShopCenter.Web.Controllers
{
    public class CartController : Controller
    {
        public CartController(UnitOfWork unitOfWork, Cart cart)
        {
            _unitOfWork = unitOfWork;
            _cart = cart;
        }
        private UnitOfWork _unitOfWork { get; set; }

        public Cart _cart { get; set; }

        public IActionResult Index(string returnUrl2)
        {
            CartIndexViewModel model = new CartIndexViewModel
            {
                _cart = _cart,
                _returnUrl = returnUrl2
            };
            return View("Index", model);
        }
        public async Task<IActionResult> AddToCart(int ProductID, string returnUrl2)
        {
            Product product = await _unitOfWork.ProductRepo.GetByID(ProductID);
            if (product != null)
            {
                _cart.AddItem(product, 1);
            }
            return RedirectToAction(nameof(Index), new { returnUrl2 });
        }
        public async Task<IActionResult> RemoveFromCart(int ProductID, string returnUrl)
        {
            Product product = await _unitOfWork.ProductRepo.GetByID(ProductID);
            if (product != null)
            {
                _cart.RemoveCart(product);
            }
            return RedirectToAction(nameof(Index), returnUrl);
        }
        // private Cart GetFromSession (string key) {
        //     return HttpContext.Session.GetObject<Cart> (key) ?? new Cart ();
        // }
        // private void SetInSession (string key, object value) {
        //     HttpContext.Session.SetJson (key, value);
        // }
    }
}