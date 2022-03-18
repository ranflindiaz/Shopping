using Shopping.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class StateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "State")]
        [MaxLength(50, ErrorMessage = "The field {0} have to have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        public int CountryId { get; set; }
    }
}
