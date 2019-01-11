using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Dal.EF;
using ShopCenter.Web.Areas.Admin.Models;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        public ProductController(UnitOfWork unitOfWork, IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        private UnitOfWork _unitOfWork { get; set; }
        public int PageSize { get; set; } = 3;

        public IHostingEnvironment _env { get; set; }
        public async Task<IActionResult> Index(string Category, int PageNumber = 1)
        {
            ProductViewModel model = new ProductViewModel();
            model.CurrentCategory = Category;
            model.list = await _unitOfWork.ProductRepo.GetPagedProductAsync(Category, PageSize, PageNumber);
            return View("Index", model);
        }
        public async Task<IActionResult> Edit(int ProductId, string returnUrl)
        {
            ProductViewModel model = new ProductViewModel();
            model.returnUrl = returnUrl;
            model.product = (await _unitOfWork.ProductRepo.GetByID(ProductId));
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel product, IFormFile files, string returnUrl)
        {
            if (files?.Length > 0)
            {
                string ImgNameFormat = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + Path.GetFileName(files.FileName);
                //string filepath = $"{_env.WebRootPath}\\images\\{ImgNameFormat}";
                string path = Path.Combine(_env.WebRootPath, "images");
                string filepath = Path.Combine(path, ImgNameFormat);
                string ExisFile = $"{_env.WebRootPath}\\images\\{product.ImgUrl}";
                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    if (System.IO.File.Exists(ExisFile))
                    {
                        System.IO.File.Delete(ExisFile);
                    }
                    files.CopyToAsync(stream).Wait();
                }
                product.ImgUrl = ImgNameFormat;
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepo.SaveProduct(product);
                TempData["Massege"] = $"{product.PName}ذخیره شد";
                return Redirect(returnUrl);
            }
            return View(product);
        }
        public IActionResult Delete(int ProductId, string returnUrl)
        {
            try
            {
                if (ProductId != 0)
                {
                    _unitOfWork.ProductRepo.DeleteProduct(ProductId);
                    TempData["Message"] = "با موفقیت حذف گردید";
                }
            }
            catch (System.Exception ex)
            {

                TempData["Error"] = ex.Message;
            }

            return Redirect(returnUrl);
        }
        public IActionResult Create(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
    }
}