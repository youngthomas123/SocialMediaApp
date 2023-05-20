using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class EditPostVM
    {
        public EditPostVM() { }

        [Required]
        public string Title { get; set; }

        public string? Body { get; set; }

        public string? ImageUrl { get; set; }
    }
}
