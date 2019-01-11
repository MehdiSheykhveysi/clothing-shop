using System;
using System.Collections.Generic;
using Shop.Domain.Entities;

namespace ShopCenter.Web.Areas.Admin.Models {
    public class ProductViewModel : Product {
        public string CurrentCategory { get; set; }
        public string returnUrl { get; set; }
        public Product product { get; set; }
        public PageedList<Product> list { get; set; } = new PageedList<Product> ();
    }
}