using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;

namespace SocialMediaWebApp.Pages
{
	[Authorize]
	public class ReportPostModel : PageModel
    {

        private readonly IPostContainer _postContainer;

        public Guid PostId { get; set; }    
		public bool IsPostIdValid { get; set; }

        public List<ReportReasonsDto> ReportReasons { get; set; }

        [BindProperty]
        public ReportPostVM ReportPostVM { get; set; }

		public ReportPostModel(IPostContainer postContainer)
        {
			_postContainer = postContainer;

		}
        public void OnGet(Guid? postId)
        {
            if(postId.HasValue)
            {
                ReportPostVM = new ReportPostVM();
                ReportReasons = _postContainer.LoadReportReasonsDtos();

                PostId = postId.Value;
                IsPostIdValid = true;
			}
            else
            {
				TempData["Error"] = "No valid Post Id supplied";
				IsPostIdValid = false;
			}
        }

        public IActionResult OnPostReportPost(Guid postId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            if (ModelState.IsValid)
            {
                
                try
                {
                    _postContainer.ReportPost(postId, userId, ReportPostVM.ReasonId);

                    TempData["ReportStatus"] = "Post reported successfully";
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
            return RedirectToPage("/ReportPost", new { postId });    
        }
    }
}
