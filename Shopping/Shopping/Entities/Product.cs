using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} have to be {1} charactérs.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "The field {0} have to be {1} charactérs.")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Stock { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        [Display(Name = "Categoríes")]
        public int CategoriesNumber => ProductCategories == null ? 0 : ProductCategories.Count;

        public ICollection<ProductImage> ProductImages { get; set; }

        [Display(Name = "Pictures")]
        public int ImagesNumber => ProductImages == null ? 0 : ProductImages.Count;

        //TODO: Pending to change to the correct path
        [Display(Name = "Picture")]
        public string ImageFullPath => ProductImages == null || ProductImages.Count == 0
            ? $"https://localhost:7245/img/noimage.png"
            : ProductImages.FirstOrDefault().ImageFullPath;

        public ICollection<OrderDetail> SaleDetails { get; set; }
    }
}
