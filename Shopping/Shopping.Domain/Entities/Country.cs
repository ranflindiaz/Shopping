using System.ComponentModel.DataAnnotations;

namespace Shopping.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Country")]
        [MaxLength(50, ErrorMessage = "The field {0} have to have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<State> States { get; set; }

        [Display(Name = "States")]
        public int StatesNumber => States == null ? 0 : States.Count;
    }
}
