using System.ComponentModel.DataAnnotations;

namespace Shopping.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7245/img/noimage.png"
            : $"https://shopping1.blob.core.windows.net/products/{ImageId}";

    }
}
