using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCenter.Web.Areas.Admin.Models
{
    public class AreaOrderViewModel
    {
        public PageedList<Order> List { get; set; } = new PageedList<Order>();
    }
}
