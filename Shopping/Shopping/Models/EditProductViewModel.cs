using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} have to be {1} charactérs maximum.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The field {0} have to be {1} charactérs maximum.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Stock { get; set; }

    }
}
