using System;

namespace Shop.Domain.Entities {
    public class PayInput {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int MerchantId { get; set; }
        public int Amount { get; set; }
    }
}