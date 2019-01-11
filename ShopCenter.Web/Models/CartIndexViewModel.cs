using Shop.Domain.Entities;

namespace ShopCenter.Web.Models {
    public class CartIndexViewModel {
        public Cart _cart { get; set; }
        public string _returnUrl { get; set; }
    }
}