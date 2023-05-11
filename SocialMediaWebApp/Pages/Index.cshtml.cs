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
			postDtos = _postContainer.GetPostPageDtos();

			

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
				_postContainer.UpdatePost(post, userId);

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
			_postContainer.UpdatePost(post, userId);
			return RedirectToPage();
		}
		public IActionResult OnPostViewComments(Guid postId)
		{
			

			return RedirectToPage("/Comments", new { PostId = postId });

			

		}
	}
}
