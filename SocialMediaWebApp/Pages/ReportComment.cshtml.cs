using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class ReportCommentModel : PageModel
    {
        private readonly ICommentContainer _commentContainer;

        public Guid CommentId { get; set; }

        public bool IsCommentIdValid { get; set; }

		public List<ReportReasonsDto> ReportReasons { get; set; }

        [BindProperty]
        public ReportCommentVM ReportCommentVM { get; set; }

		public ReportCommentModel(ICommentContainer commentContainer)
        {
            _commentContainer = commentContainer;
        }

        public void OnGet(Guid? commentId)
        {
            if(commentId.HasValue)
            {
				ReportCommentVM = new ReportCommentVM();
				ReportReasons = _commentContainer.LoadReportReasonsDtos();

				CommentId = commentId.Value;
				IsCommentIdValid = true;
			}
            else
            {
				TempData["Error"] = "No valid Post Id supplied";
				IsCommentIdValid = false;
			}
        }

        public IActionResult OnPostReportComment(Guid commentId)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			if (ModelState.IsValid)
			{

				try
				{
					_commentContainer.ReportComment(commentId, userId, ReportCommentVM.ReasonId);

					TempData["ReportStatus"] = "Comment reported successfully";
				}
				catch (AccessException ex)
				{
					TempData["ReportStatus"] = ex.Message;

				}
				catch (ItemNotFoundException ex)
				{
					TempData["ReportStatus"] = ex.Message;
				}
			}

			return RedirectToPage("/ReportComment", new { commentId });
		}
    }
}
