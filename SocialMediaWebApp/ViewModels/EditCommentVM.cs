using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    public class EditCommentVM
    {

        public EditCommentVM() { }

        [Required]
        public string Body { get; set; }
    }
}
