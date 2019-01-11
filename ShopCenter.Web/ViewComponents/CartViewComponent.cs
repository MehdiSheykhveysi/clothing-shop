using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Entities;

namespace ShopCenter.Web.ViewComponents
{
    public class CartViewComponent:ViewComponent
    {
        public Cart _cart { get; set; }
        public CartViewComponent(Cart cart)
        {
            _cart=cart;
        }
        public  IViewComponentResult Invoke() =>  View("Default",_cart);
    }
}