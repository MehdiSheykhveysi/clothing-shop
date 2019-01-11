using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Dal.EF;

namespace ShopCenter.Web.ViewComponents
{
    [ViewComponent]
    public class CategoryViewComponent : ViewComponent
    {
        public CategoryViewComponent(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UnitOfWork _unitOfWork { get; set; }


        public async Task<IViewComponentResult> InvokeAsync(string controller)
        {
            var model=await _unitOfWork.ProductRepo.GetAllCategoryasync();
            ViewData["controller"]=controller;
            return View("Default",model);
        }
    }
}