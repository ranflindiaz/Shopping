using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class AddCategoryProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a category.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}
