using System.ComponentModel.DataAnnotations;

namespace Shopping.Domain.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "State")]
        [MaxLength(50, ErrorMessage = "The field {0} have to have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }

        [Display(Name = "Cities")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
