using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shop.Domain.Entities;
using ShopCenter.Web.Infrastracture;

namespace ShopCenter.Web.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart Cart = session?.GetObject<SessionCart>("cart") ?? new SessionCart();
            Cart.Session = session;
            return Cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Product product, int Quentity)
        {
            base.AddItem(product, Quentity);
            Session.SetJson("cart", this);
        }
        public override void RemoveCart(Product product)
        {
            base.RemoveCart(product);
            Session.SetJson("cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("cart");
        }
    }
}