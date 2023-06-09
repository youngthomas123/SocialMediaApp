using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class LoginVM
    {

        public LoginVM() { }

        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
