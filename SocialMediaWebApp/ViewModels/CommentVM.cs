using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class CommentVM
    {
        public CommentVM() { }

        [Required]
        [MaxLength(350)]
        public string Body { get; set; }
    }
}
