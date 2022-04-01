using Shopping.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class AddUserViewModel : EditUserViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Type a valid email.")]
        [MaxLength(100, ErrorMessage = "Field {0} have to have {1} charactérs.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} should be between {2} and {1} charácters.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Password Confirmatión are not the same.")]
        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} should be between {2} and {1} charácters.")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType
        {
            get; set;

        }
    }
}
