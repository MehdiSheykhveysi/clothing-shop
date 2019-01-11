using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Dal.EF;
using Shop.Domain.Entities;
using ShopCenter.Web.Areas.Admin.Models;

namespace ShopCenter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        public OrderController(UnitOfWork unitOfWork, IServiceProvider service)
        {
            _unitOfWork = unitOfWork;
        }
        public UnitOfWork _unitOfWork { get; set; }
        private int PageSize { get; } = 3;

        public async Task<ViewResult> NotDelivered(int PageNumber = 1)
        {
            AreaOrderViewModel model = new AreaOrderViewModel();
            var query = await _unitOfWork.OrderRepo.GetPagedListAsync(PageNumber, PageSize, false);
            model.List = query;
            model.List.pageData = query.pageData;
            model.List.pageData.CurentItem = PageNumber;
            return View("NotDelivered", model);
        }
        [HttpPost]
        public async Task<RedirectToActionResult> MarkShipped(int OrderID, int pageNumber = 1)
        {
            Order order = _unitOfWork.OrderRepo.GetById(OrderID);
            if (order != null)
            {
                order.Shipped = true;
                TempData["Massege"] = "Order Delivered.!";
                _unitOfWork.OrderRepo.SaveOrder(order);
            }
            pageNumber = await HasPageResult(pageNumber);
            return RedirectToAction(nameof(NotDelivered), new { PageNumber = pageNumber });
        }
        private async Task<int> HasPageResult(int pageNumber)
        {
            var result = await _unitOfWork.OrderRepo.GetPagedListAsync(pageNumber, PageSize, false);
            int count = result.ListItem.Count;
            if (count == 0 && pageNumber > 1)
            {
                pageNumber -= 1;
            }
            return pageNumber;
        }
    }
}