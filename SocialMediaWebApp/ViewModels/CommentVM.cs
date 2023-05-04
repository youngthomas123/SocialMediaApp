using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class CommentVM
    {
        public CommentVM() { }

        [Required]
        public string Body { get; set; }
    }
}
