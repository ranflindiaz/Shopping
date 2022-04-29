using Shopping.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class AddProductToCartViewModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} maximum characters is {1}.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "The field {0} maximum characters is {1}.")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Stock { get; set; }

        public string Categories { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Range(0.0000001, float.MaxValue, ErrorMessage = "Type a value higher than  cero (0).")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string Remarks { get; set; }
    }

}
