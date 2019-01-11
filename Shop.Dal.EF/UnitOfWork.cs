using System;
using Shop.Dal.EF.Repositories;
using Shop.Domain.Contract.Repositories;
using Shop.Domain.Contrect.Repositories;

namespace Shop.Dal.EF {
    public class UnitOfWork:IDisposable {
        public UnitOfWork (IProductRepository producRepo,ShopCenterDBContext Context,IOrderRepsitory orderRepo ) {
            _producRepo=producRepo;
            _context = Context;
            _orderRepo=orderRepo;
        }
        // public UnitOfWork(ShopCenterDBContext Context,ProductRepository producRepo)
        // {
        //      _context = Context;
        //      _producRepo=producRepo;
        // }
        private readonly ShopCenterDBContext _context ;
        private readonly IProductRepository _producRepo ;
        private readonly IOrderRepsitory _orderRepo ;
        public IProductRepository ProductRepo {
            get { return _producRepo; }
        }
        public IOrderRepsitory OrderRepo {
            get { return _orderRepo; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}