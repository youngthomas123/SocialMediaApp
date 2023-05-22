using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class EditProfileVM
    {
        public EditProfileVM() { }

        [MaxLength(200)]
        [Required]
        public string Bio { get; set; }

        [MaxLength(20)]
        [Required]
        public string Gender { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProfilePic { get; set; }

        [MaxLength(50)]
        [Required]
        public string Location { get; set; }
    }
}
