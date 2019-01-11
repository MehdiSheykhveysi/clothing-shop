using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Domain.Contrect.Repositories {
    public interface IProductRepository {
        Task<IEnumerable<Product>> GetListasync();
        Task<IEnumerable<string>> GetAllCategoryasync();
        Task<Product> GetByID(int ProducTID);
        Task<PageedList<Product>> GetPagedProductAsync(string Category,int PageSize,int PageNumber);
        void SaveProduct(Product product);
        void DeleteProduct(int ProducTID);
        Task<List<Product>> Search(string word);
        
    }
}