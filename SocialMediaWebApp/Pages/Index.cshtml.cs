﻿using Microsoft.AspNetCore.Authorization;
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

		public IActionResult OnPostUpvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}
			else
			{
				post.Upvote();
				post.AddUpvotedUserId(userId);
			
				_postContainer.UpdatePostScore(post, userId, direction);

				return RedirectToPage();
			}

		}
		public IActionResult OnPostRemoveUpvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}
			else
			{
				post.RemoveUpvote();
				post.RemoveUpvotedUserId(userId);
				
				_postContainer.UpdatePostScore(post, userId, direction);

				return RedirectToPage();
			}

		}


		public IActionResult OnPostDownvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}
			post.Downvote();
			post.AddDownvotedUserId(userId);
			
			_postContainer.UpdatePostScore(post, userId, direction);
			return RedirectToPage();
		}

		public IActionResult OnPostRemoveDownvote(Guid postId, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}

			post.RemoveDownvote();
			post.RemoveDownvotedUserId(userId);
			
			_postContainer.UpdatePostScore(post, userId, direction);
			return RedirectToPage();

		}





		public IActionResult OnPostViewComments(Guid postId)
		{
			

			return RedirectToPage("/Comments", new { PostId = postId });

			

		}
	}
}
