using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SocialMedia.BusinessLogic;
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

		public bool AlreadyUpvoted { get; set; }

		public IndexModel(ILogger<IndexModel> logger, IPostContainer postContainer)
		{
			_logger = logger;
			_postContainer = postContainer;

		}

		public void OnGet()
		{
			postDtos = _postContainer.GetPostPageDtos(Guid.Parse(User.FindFirst("UserId").Value));

			

		}

		public void OnPost()
		{


		}

		public IActionResult OnPostUpvote(Guid postId)
		{
			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}
			else
			{
				post.upvote();
				var userId = Guid.Parse(User.FindFirst("UserId").Value);
				_postContainer.UpdatePostScore(post, userId, "up");

				return RedirectToPage();
			}

		}
		public IActionResult OnPostRemoveUpvote(Guid postId)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}
			else
			{
				post.downvote();
				post.RemoveUpvotedUserId(userId);
				
				_postContainer.UpdatePostScore(post, userId, "removeup");

				return RedirectToPage();
			}

		}


		public IActionResult OnPostDownvote(Guid postId)
		{
			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}
			post.downvote();
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			_postContainer.UpdatePostScore(post, userId, "down");
			return RedirectToPage();
		}

		public IActionResult OnPostRemoveDownvote(Guid postId)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}

			post.upvote();
			post.RemoveDownvotedUserId(userId);
			
			_postContainer.UpdatePostScore(post, userId, "removedown");
			return RedirectToPage();

		}





		public IActionResult OnPostViewComments(Guid postId)
		{
			

			return RedirectToPage("/Comments", new { PostId = postId });

			

		}
	}
}
