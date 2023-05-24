using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.DataAccess;
using SocialMediaWebApp.ViewModels;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class EditCommentModel : PageModel
    {
        private readonly ICommentContainer _commentContainer;

        [BindProperty]
        public EditCommentVM EditCommentVM { get; set; }

        public Comment Comment { get; set; }

        public EditCommentModel(ICommentContainer commentContainer)
        {
            _commentContainer = commentContainer;
        }

        public void OnGet(Guid CommentId)
        {
            EditCommentVM = new EditCommentVM();


            Comment = _commentContainer.LoadCommentById(CommentId);

            EditCommentVM.Body = Comment.Body;


            
        }

        public IActionResult OnPostEditComment(Guid CommentId)
        {
            if(ModelState.IsValid)
            {
                _commentContainer.UpdateComment(CommentId, EditCommentVM.Body);
            }
            return RedirectToPage("/EditComment", new { CommentId });
        }
    }
}
