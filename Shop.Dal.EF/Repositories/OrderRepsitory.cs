using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Contract.Repositories;
using Shop.Domain.Entities;

namespace Shop.Dal.EF.Repositories
{
    public class OrderRepsitory : IOrderRepsitory
    {
        public ShopCenterDBContext _Context { get; set; }
        public OrderRepsitory(ShopCenterDBContext Context)
        {
            _Context = Context;
        }
        public Order GetById(int OrderID)
        {
            return _Context.Orders.Include(c => c.Lines).ThenInclude(l => l.product).FirstOrDefault(c => c.ID == OrderID);
        }

        public void SaveOrder(Order order)
        {
            _Context.AttachRange(order.Lines.Select(l => l.product));
            if (order.ID == 0)
            {
                _Context.Orders.Add(order);
            }
            _Context.SaveChanges();
        }

        public void SetPaymentDone(string transID)
        {
            var order = _Context.Orders.FirstOrDefault(c => c.PeymentId == transID);
            if (order != null)
            {
                order.PaymentDate = DateTime.Now;
                _Context.SaveChanges();
            }
        }

        public void SetTransactionID(int orderID, string transID)
        {
            var order = _Context.Orders.Find(orderID);
            if (order != null)
            {
                order.PeymentId = transID;
                _Context.SaveChanges();
            }
        }


        public async Task<PageedList<Order>> GetPagedListAsync(int PageNumber, int PageSize, bool Shipped)
        {
            PageedList<Order> pageed = new PageedList<Order>();
            var result = await GetListAsync(false);
            pageed.ListItem = result.OrderBy(c => c.ID).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            pageed.pageData.TotalItem = result.Count;
            pageed.pageData.ItemPerPage = PageSize;
            return pageed;
        }

        public async Task<List<Order>> GetListAsync(bool? Shipped)
        {
            return await _Context.Orders.Where(c => !Shipped.HasValue || c.Shipped == Shipped.Value).Include(c => c.Lines).ThenInclude(p => p.product).ToListAsync();
        }
    }
}