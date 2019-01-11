using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shop.Domain.Entities
{
    public class Order
    {
        public virtual int ID { get; set; }
        public virtual ICollection<CartLine> Lines { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Country { get; set; }
        public virtual bool GiftWrap { get; set; }
        public virtual string PeymentId { get; set; }
        public virtual DateTime? PaymentDate { get; set; }
        public virtual bool Shipped { get; set; }

    }
}