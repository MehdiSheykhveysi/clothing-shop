using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Dal.EF;
using Shop.Domain.Entities;
using ShopCenter.Web.Models;
//using ShopCenter.Web.Models;

namespace ShopCenter.Web.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(UnitOfWork unitOfWork, Cart cart)
        {
            _unitOfWork = unitOfWork;
            _cart = cart;
        }

        private UnitOfWork _unitOfWork { get; set; }
        public Cart _cart { get; set; }
        public ActionResult Checkout()
        {
            return View(new OrderViewModel());
        }

        [HttpPost]
        public ActionResult Checkout(OrderViewModel order)
        {
            if (_cart.List.Count == 0)
            {
                ModelState.AddModelError("", "امکان ثبت سفارش خالی وجود ندارد");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.List;
                _unitOfWork.OrderRepo.SaveOrder(order);
                TempData["OrderId"] = order.ID;
                return RedirectToAction(nameof(Completed), order);
            }
            else
            {
                return View(order);
            }
        }

        public IActionResult Completed(OrderViewModel model)
        {
            PayInput model2 = new PayInput();
            if (ModelState.IsValid)
            {
                model.Lines = _cart.List;
                model2.OrderId = TempData["OrderId"] != null ? (int)TempData["OrderId"] : 0;
                model2.Amount = Convert.ToInt32(model.Lines.Sum(c => c.product.Price * c.Quentity));
                _cart.Clear();
            }
            return View(model2);
        }
    }
}