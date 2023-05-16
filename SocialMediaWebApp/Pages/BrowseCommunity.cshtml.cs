using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class BrowseCommunityModel : PageModel
    {
        private readonly IPostContainer _postContainer;
		private readonly ICommunityContainer _communityContainer;
        

		 public CommunityFullDto Community{ get; set; }

        public List<PostPageDto> PostDtos { get; set; }
		
		
		public BrowseCommunityModel(IPostContainer postContainer, ICommunityContainer communityContainer)
        {
            _postContainer = postContainer;
            
			_communityContainer = communityContainer;
        }

        public void OnGet(string CommunityName)
        {
			

			
            var userId = Guid.Parse(User.FindFirst("UserId").Value);


			Community = _communityContainer.LoadCompleteCommunityDto(CommunityName);

			
            PostDtos = _postContainer.GetPostPageDtosByCommunity(Community.CommunityId, userId);
			

        }

		public IActionResult OnPostUpvote(Guid postId, string CommunityName)
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

				return RedirectToPage("/BrowseCommunity",  new { CommunityName = CommunityName });
			}

		}

		public IActionResult OnPostDownvote(Guid postId, string CommunityName)
		{
			var post = _postContainer.LoadPostById(postId);
			if (post == null)
			{
				return NotFound();
			}
			post.downvote();
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			_postContainer.UpdatePostScore(post, userId, "down");
			return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}
		public IActionResult OnPostViewComments(Guid postId)
		{
			

			return RedirectToPage("/Comments", new { PostId = postId });



		}
		public IActionResult OnPostFollowCommunity(string CommunityName, Guid communityId)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			_communityContainer.FollowCommunity(communityId, userId);

			return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}
		public IActionResult OnPostUnfollowCommunity(string CommunityName, Guid communityId)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			_communityContainer.UnfollowCommunity(communityId, userId);

			return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}

	}
}
