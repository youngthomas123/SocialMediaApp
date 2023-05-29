using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
    [Authorize]
    public class EditCommentVM
    {

        public EditCommentVM() { }

        [Required]
        [MaxLength(350)]
        public string Body { get; set; }
    }
}
