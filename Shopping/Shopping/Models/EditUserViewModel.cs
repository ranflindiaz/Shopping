using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "Field {0} have to have {1} charactérs.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Field {0} have to have {1} charactérs.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Field {0} have to have {1} charactérs.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [MaxLength(200, ErrorMessage = "Field {0} have to have {1} charactérs.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(20, ErrorMessage = "Field {0} have to have {1} charactérs.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Picture")]
        public Guid ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Picture")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7245/img/noimage.png"
            : $"https://shopping1.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Country.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "State")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a State.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public int StateId { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a City.")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

    }
}
