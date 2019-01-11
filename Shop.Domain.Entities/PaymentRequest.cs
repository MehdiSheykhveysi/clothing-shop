using System;

namespace Shop.Domain.Entities
{
    public class PaymentRequest
    {
        public int Status { get; set; }
        public string Authority { get; set; }
        public string Link { get; set; }
    }
}
