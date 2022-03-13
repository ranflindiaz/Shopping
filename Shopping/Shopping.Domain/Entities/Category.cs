using System.ComponentModel.DataAnnotations;

namespace Shopping.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        [MaxLength(50, ErrorMessage = "The field {0} have to have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }
    }
}
