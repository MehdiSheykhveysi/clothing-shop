using System;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Contract;
using Shop.Domain.Entities;

namespace ShopCenter.Web.Payment
{
    public class PaymentClass : IPayment
    {

        private ZarinpalSandbox.Payment _payment { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public PaymentClass(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public PaymentRequest Pay(PayInput input)
        {
            _payment = new ZarinpalSandbox.Payment(Convert.ToInt32(input.Amount));
            // var result = _payment.PaymentRequest(input.OrderId + input.Description, "https://localhost:5001/Payment/Verify", input.Email, input.Mobile);
            var result = _payment.PaymentRequest(input.OrderId + input.Description, "https://localhost:5001/Payment/Verify");
            PaymentRequest pay = new PaymentRequest();
            if (result.IsCompletedSuccessfully)
            {
                _session.SetInt32(result.Result.Authority, input.Amount);

                pay.Authority = result.Result.Authority;
                pay.Link = result.Result.Link;
                pay.Status = result.Result.Status;

            }
            return pay; ;
        }

        public VerifyResponse Verify(string Authority)
        {
            VerifyResponse verify = new VerifyResponse();
            if (!string.IsNullOrEmpty(Authority))
            {
                _payment = new ZarinpalSandbox.Payment(Convert.ToInt32(_session.GetInt32(Authority)));
                _session.Clear();
                var result = _payment.Verification(Authority);
                if (result.Result.Status == 100)
                {
                    verify.Status = result.Result.Status;
                    verify.RefId = result.Result.RefId;
                    verify.Authority = Authority;
                }
            }
            return verify;

        }
    }
}