using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.Domain.Entities;

namespace ShopCenter.Web.Models {
    public class OrderViewModel : Order {
        [BindNever]
        public override int ID { get; set; }
        [BindNever]
        public override ICollection<CartLine> Lines { get; set; }
        [Required(ErrorMessage="Please Enter Name")]
        public override string Name { get; set; }
        [Required(ErrorMessage="Please Enter City Name")]
        public override string City { get; set; }
        [Required(ErrorMessage="Please Enter State Name")]
        public override string State { get; set; }
        [Required(ErrorMessage="Please Enter Phone Number")]
        public override string Phone { get; set; }
        [Required(ErrorMessage="Please Enter Country Name")]
        public override string Country { get; set; }
        public override bool GiftWrap { get; set; }
        [MaxLength(100)]
        public override string PeymentId { get; set; }
        public override DateTime? PaymentDate { get; set; }
        public override bool Shipped { get; set; }
    }
}