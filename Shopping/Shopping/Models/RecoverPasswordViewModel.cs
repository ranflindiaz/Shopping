using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class RecoverPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field {0} is requried.")]
        [EmailAddress(ErrorMessage = "Type a valid email.")]
        public string Email { get; set; }

    }
}
