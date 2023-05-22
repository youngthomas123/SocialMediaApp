using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class EditPostVM
    {
        public EditPostVM() { }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        
        [MaxLength(750)]
        public string? Body { get; set; }

        public string? ImageUrl { get; set; }
    }
}
