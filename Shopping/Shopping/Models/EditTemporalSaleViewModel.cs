using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class EditTemporalSaleViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Range(0.0000001, float.MaxValue, ErrorMessage = "Type a value higher than  cero (0).")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public float Quantity { get; set; }

    }
}
