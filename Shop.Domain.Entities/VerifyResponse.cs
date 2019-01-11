using System;

namespace Shop.Domain.Entities
{
    public class VerifyResponse
    {
        public int Status { get; set; }
        public long RefId { get; set; }
        public string Authority { get; set; }
    }
}
