using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Actual Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} have to be between {2} and {1} charácters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} have to be between {2} and {1} charácters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "New password and password confirmation are not equals.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password Confirmation")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} have to be between {2} and {1} charácters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Confirm { get; set; }

    }
}
