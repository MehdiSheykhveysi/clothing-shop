using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Contrect.Repositories;
using Shop.Domain.Entities;

namespace Shop.Dal.EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ShopCenterDBContext Context)
        {
            _context = Context;
        }
        private ShopCenterDBContext _context { get; set; }

        public void DeleteProduct(int ProducTID)
        {
            try
            {
                if (ProducTID != 0)
                {
                    Product product = _context.Products.FirstOrDefault(c => c.ID == ProducTID);
                    if (product != null)
                    {
                        _context.Entry(product).State = EntityState.Deleted;
                        _context.SaveChanges();
                    }
                }
            }
            catch
            {
                throw new System.Exception("متاسفانه این محصول را نمیتوانید حذف کنید.");
            }

        }

        public async Task<IEnumerable<string>> GetAllCategoryasync()
        {
            return await _context.Products.Select(c => c.Category).Distinct().ToListAsync();
        }

        public async Task<Product> GetByID(int ProductID)
        {
            return await _context.Products.FindAsync(ProductID);
        }

        public async Task<IEnumerable<Product>> GetListasync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<PageedList<Product>> GetPagedProductAsync(string Category, int PageSize, int PageNumber)
        {
            var query = _context.Products.Where(c => string.IsNullOrWhiteSpace(Category) || c.Category.Equals(Category));
            PageedList<Product> Result = new PageedList<Product>();
            Result.pageData.TotalItem = query.Count();
            Result.pageData.ItemPerPage = PageSize;
            Result.pageData.CurentItem = PageNumber;
            Result.ListItem = await query.OrderBy(c => c.ID).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            return Result;
        }

        public void SaveProduct(Product product)
        {
            if (product.ID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product pro = _context.Products.FirstOrDefault(p => p.ID == product.ID);
                if (pro != null)
                {
                    pro.PName = product.PName;
                    pro.Price = product.Price;
                    pro.Description = product.Description;
                    pro.Category = product.Category;
                    pro.ImgUrl = product.ImgUrl;
                }
            }
            _context.SaveChanges();
        }

        public async Task<List<Product>> Search(string word)
        {
            return await _context.Products.Where(c => string.IsNullOrWhiteSpace(word) || c.Category.Equals(word)).ToListAsync();
        }
        // public async Task<PagedListData<Product>> GetPageDataAsync (string Category, int PageNumber, int PageSize) {
        //     var query = _context.Products.Where (c => string.IsNullOrWhiteSpace (c.Category) || c.Category == Category);
        //     PagedListData<Product> Result = new PagedListData<Product> ();
        //     Result.pagingData.CurrentPage = PageNumber;
        //     Result.pagingData.ItemsPerPage = PageSize;
        //     Result.pagingData.TotalItems = await query.CountAsync ();
        //     Result.Items = await query.OrderBy (c => c.ID).Skip ((PageNumber - 1) * PageSize).Take (PageSize).ToListAsync ();
        //     return Result;
        // }
    }
}