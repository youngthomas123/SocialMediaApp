using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class SignUpVM
    {
        public SignUpVM() { }

        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }



    }
}
