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
        
		public string Communityname { get; set; }

        public List<PostPageDto> PostDtos { get; set; }
		public List<string> Rules { get; set; }
		public bool AlreadyUpvoted { get; set; }

		public BrowseCommunityModel(IPostContainer postContainer, ICommunityContainer communityContainer)
        {
            _postContainer = postContainer;
            
			_communityContainer = communityContainer;
        }

        public void OnGet(string CommunityName)
        {
			

			Communityname = CommunityName;

            var communityId = _communityContainer.GetCommunityId(Communityname);
            PostDtos = _postContainer.GetPostPageDtosByCommunity(new Guid(communityId));
			Rules = _communityContainer.GetCommunityRules(new Guid(communityId));

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
				_postContainer.UpdatePost(post, userId);

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
			_postContainer.UpdatePost(post, userId);
			return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}
		public IActionResult OnPostViewComments(Guid postId)
		{
			

			return RedirectToPage("/Comments", new { PostId = postId });



		}
	}
}
