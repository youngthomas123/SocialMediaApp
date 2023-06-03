using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using System.Net;

namespace SocialMediaWebApp.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private IPostContainer _postContainer;
		public List<PostPageDto> postDtos;

		

		public IndexModel(ILogger<IndexModel> logger, IPostContainer postContainer)
		{
			_logger = logger;
			_postContainer = postContainer;

		}

		public void OnGet()
		{

            var userId = Guid.Parse(User.FindFirst("UserId").Value);


            postDtos = _postContainer.GetPostPageDtos(userId);

			

		}

		public void OnPost()
		{


		}

		public IActionResult OnPostUpvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			
			try
			{
				_postContainer.Upvote(postId, direction, userId);
                return RedirectToPage();
            }
			catch(ItemNullException)
			{
                return NotFound();
            }


		}
		public IActionResult OnPostRemoveUpvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.RemoveUpvote(postId, direction, userId);
                return RedirectToPage();
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }


		public IActionResult OnPostDownvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.Downvote(postId, direction, userId);
                return RedirectToPage();
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }

		public IActionResult OnPostRemoveDownvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.RemoveDownvote(postId, direction, userId);
                return RedirectToPage();
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }


		public IActionResult OnPostViewComments(Guid postId)
		{
			

			return RedirectToPage("/Comments", new { PostId = postId });

		}

		public IActionResult OnPostDeletePost(Guid PostId)
		{
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

			try
			{
                _postContainer.DeletePost(PostId, userId);
            }
			catch(AccessException)
			{
				return BadRequest();
            }
            catch(ItemNotFoundException)
			{
                return NotFound();
            }


            return RedirectToPage();
        }

		
	}
}
