using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Entities {
    public class Product {
        public virtual int ID { get; set; }

        [Display (Name = "نام")]
        [Required (ErrorMessage = "نام باید پر شود")]
        public virtual string PName { get; set; }

        [Display (Name = "توضیحات")]
        [Required (ErrorMessage = "توضیحات باید پر شود")]
        public virtual string Description { get; set; }

        [Display (Name = "قیمت")]
        [Required (ErrorMessage = "قیمت باید پر شود")]
        public virtual decimal Price { get; set; }

        [Display (Name = "دسته بندی")]
        [Required (ErrorMessage = "دسته بندی باید پر شود")]
        public virtual string Category { get; set; }

        [Display (Name = "عکسی")]
        public virtual string ImgUrl { get; set; }
    }
}