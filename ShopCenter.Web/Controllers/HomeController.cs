using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Dal.EF;
using Shop.Domain.Entities;
using ShopCenter.Web.Models;

namespace ShopCenter.Web.Controllers {
    public class HomeController : Controller {
        private int PageSize { get; set; } = 2;
        private UnitOfWork _unitOfWork { get; set; }
        public HomeController (UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index (string Category, int PageNumber = 1) {
            HomeViewModel model = new HomeViewModel ();
            model.list = await _unitOfWork.ProductRepo.GetPagedProductAsync (Category, PageSize, PageNumber);
            model.CurentCategory = Category;
            return View (model);
        }
        public IActionResult About () {
            return View ();
        }
        public IActionResult Contact () {
            return View ();
        }
        public IActionResult Error () {
            return View ();
        }
        public async Task<IActionResult> Search (string Word) {
            ViewData["Term"]=Word;
            IEnumerable<Product> model = await _unitOfWork.ProductRepo.Search (Word);
            return View (model);
        
        }
    }
}