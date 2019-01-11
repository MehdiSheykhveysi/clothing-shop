using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Domain.Entities
{
    public class Cart
    {
        public List<CartLine> List { get; set; } = new List<CartLine>();
        public virtual void AddItem(Product product, int Quentity)
        {
            CartLine line = List.Where(c => c.product.ID == product.ID).FirstOrDefault();
            if (line == null)
            {
                List.Add(new CartLine
                {
                    product = product,
                    Quentity = Quentity
                });
            }
            else
            {
                line.Quentity += Quentity;
            }
        }
        public virtual void RemoveCart(Product product) => List.RemoveAll(c => c.product == product);
        public virtual void Clear() => List.Clear();
    }
    public class CartLine
    {
        public int ID { get; set; }
        public Product product { get; set; }
        public int CartId { get; set; }
        public int Quentity { get; set; }
    }
}