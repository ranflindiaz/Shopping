using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Type a valid email.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} have to be between {2} and {1} charácters.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "New password and confirmation are not equals.")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field {0} have to be between {2} and {1} charácters.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        public string Token { get; set; }

    }
}
