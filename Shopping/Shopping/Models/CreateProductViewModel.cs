using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CreateProductViewModel : EditProductViewModel
    {
        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Category.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Picture")]
        public IFormFile? ImageFile { get; set; }

    }
}
