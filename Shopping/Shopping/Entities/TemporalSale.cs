using System.ComponentModel.DataAnnotations;

namespace Shopping.Entities
{
    public class TemporalSale
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string? Remarks { get; set; }

    }
}
