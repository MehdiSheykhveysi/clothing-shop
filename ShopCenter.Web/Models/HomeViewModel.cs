using Shop.Domain.Entities;

namespace ShopCenter.Web.Models
{
    public class HomeViewModel
    {
        public PageedList<Product> list { get; set; } = new PageedList<Product>();
        public string CurentCategory { get; set; }
    }
}