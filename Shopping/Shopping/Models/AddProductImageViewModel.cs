using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class AddProductImageViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public IFormFile ImageFile { get; set; }
    }
}
