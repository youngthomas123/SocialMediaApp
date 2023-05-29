using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Custom_exception;
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

        public bool IsCommentIdValid { get; set; }
        [BindProperty]
        public EditCommentVM EditCommentVM { get; set; }

        public Comment Comment { get; set; }

        public EditCommentModel(ICommentContainer commentContainer)
        {
            _commentContainer = commentContainer;
        }

        public void OnGet(Guid? CommentId)
        {
            if(CommentId.HasValue)
            {
                EditCommentVM = new EditCommentVM();

                try
                {
                    Comment = _commentContainer.LoadCommentById(CommentId.Value);

                    EditCommentVM.Body = Comment.Body;

                    IsCommentIdValid = true;
                }
                catch(ItemNotFoundException ex)
                {
                    TempData["Error"] = ex.Message;
                    IsCommentIdValid = false;
                }
               
            }
            else
            {
                TempData["Error"] = "No valid Comment Id supplied";
                IsCommentIdValid = false;
            }
 

        }

        public IActionResult OnPostEditComment(Guid CommentId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            if (ModelState.IsValid)
            {
                try
                {
                    _commentContainer.UpdateComment(CommentId, EditCommentVM.Body, userId);
                    TempData["EditStatus"] = "Comment edited successfully";
                }
                catch(ItemNotFoundException ex)
                {
                    TempData["EditStatus"] = ex.Message;
                }
                catch(AccessException ex)
                {
                    TempData["EditStatus"] = ex.Message;
                }
                
            }
            return RedirectToPage("/EditComment", new { CommentId });
        }
    }
}
