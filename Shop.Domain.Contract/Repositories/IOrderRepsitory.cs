using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Domain.Contract.Repositories
{
    public interface IOrderRepsitory
    {
        Task<PageedList<Order>> GetPagedListAsync(int PageNumber, int PageSize, bool Shipped);
        Task<List<Order>> GetListAsync(bool? Shipped);
        Order GetById(int OrderID);
        void SaveOrder(Order order);
        void SetTransactionID(int orderID,string transID);
        void SetPaymentDone(string transID);
    }
}
