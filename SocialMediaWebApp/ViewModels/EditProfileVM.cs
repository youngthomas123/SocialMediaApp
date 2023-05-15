namespace SocialMediaWebApp.ViewModels
{
    public class EditProfileVM
    {
        public EditProfileVM() { }

        public string Bio { get; set; }

        public string Gender { get; set; }

        public IFormFile ProfilePic { get; set; }

        public string Location { get; set; }
    }
}
