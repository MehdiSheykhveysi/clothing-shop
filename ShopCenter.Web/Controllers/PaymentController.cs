using System;
using Microsoft.AspNetCore.Mvc;
using Shop.Dal.EF;
using Shop.Domain.Contract;
using Shop.Domain.Contract.Repositories;
using Shop.Domain.Entities;
using ShopCenter.Web.Payment;

namespace ShopCenter.Web.Controllers
{
    public class PaymentController : Controller
    {
        public PaymentController(IPayment Payment, UnitOfWork unitOfWork)
        {
            _Payment = Payment;
            _unitOfWork = unitOfWork;
        }
        public IPayment _Payment { get; set; }
        public UnitOfWork _unitOfWork { get; set; }
        public IActionResult Pay(PayInput input)
        {
            PaymentRequest result = _Payment.Pay(input);
            if (result != null && result.Status == 100)
            {
                _unitOfWork.OrderRepo.SetTransactionID(input.OrderId, result.Authority);
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Authority);
            }
            return View("PayErorr", result);
        }

        public IActionResult Verify(string Authority)
        {
            VerifyResponse result = _Payment.Verify(Authority);
            if (result != null && result.Status == 100)
            {
                _unitOfWork.OrderRepo.SetPaymentDone(Authority);
                return View("ViewComplete", result);
            }
            return View("VerifyErorr", result);
        }
    }
}