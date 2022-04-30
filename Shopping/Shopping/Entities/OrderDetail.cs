using System.ComponentModel.DataAnnotations;

namespace Shopping.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string? Remarks { get; set; }

        public Product Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => Product == null ? 0 : (decimal)Quantity * Product.Price;

    }
}
