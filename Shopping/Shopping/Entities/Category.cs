using System.ComponentModel.DataAnnotations;

namespace Shopping.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        [MaxLength(50, ErrorMessage = "The field {0} have to be {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        [Display(Name = "# Products")]
        public int ProductsNumber => ProductCategories == null ? 0 : ProductCategories.Count();
    }
}
