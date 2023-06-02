using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Custom_exception;
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
        

		public bool isCommunitynameValid { get; set; }	

        public CommunityFullDto Community{ get; set; }

        public List<PostPageDto> PostDtos { get; set; }
		
		
		public BrowseCommunityModel(IPostContainer postContainer, ICommunityContainer communityContainer)
        {
            _postContainer = postContainer;
            
			_communityContainer = communityContainer;
        }

        public void OnGet(string? CommunityName)
        {
			if(CommunityName != null)
			{
                var userId = Guid.Parse(User.FindFirst("UserId").Value);


				try
				{
                    Community = _communityContainer.LoadCompleteCommunityDto(CommunityName);


                    PostDtos = _postContainer.GetPostPageDtosByCommunity(Community.CommunityId, userId);

                    isCommunitynameValid = true;
                }
				catch(ItemNotFoundException ex)
				{
                    TempData["Error"] = ex.Message;
                    isCommunitynameValid = false;
                }
               
            }
			else
			{
                TempData["Error"] = "No communityname supplied";
                isCommunitynameValid = false;
            }
			

        }

		public IActionResult OnPostUpvote(Guid postId, string CommunityName, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.Upvote(postId, direction, userId);
                return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }

        }

		public IActionResult OnPostRemoveUpvote(Guid postId, string CommunityName, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.RemoveUpvote(postId, direction, userId);
                return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }

        }

		public IActionResult OnPostDownvote(Guid postId, string CommunityName, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.Downvote(postId, direction, userId);
                return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }

		public IActionResult OnPostRemoveDownvote(Guid postId, string CommunityName, string direction)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.RemoveDownvote(postId, direction, userId);
                return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }

		public IActionResult OnPostDeletePost(Guid PostId, string CommunityName)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			try
			{
				_postContainer.DeletePost(PostId, userId);
			}
			catch (AccessException)
			{
				return BadRequest();
			}
			catch (ItemNotFoundException)
			{
				return NotFound();
			}


			return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}

		public IActionResult OnPostReportPost(Guid postId, string CommunityName)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			try
			{
				_postContainer.ReportPost(postId, userId);
			}
			catch (AccessException)
			{
				return BadRequest();
			}
			catch (ItemNotFoundException)
			{
				return NotFound();
			}



			return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}

		public IActionResult OnPostViewComments(Guid postId)
		{
			

			return RedirectToPage("/Comments", new { PostId = postId });



		}
		public IActionResult OnPostFollowCommunity(string CommunityName, Guid communityId)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _communityContainer.FollowCommunity(communityId, userId);
            }
            catch(AccessException)
            {
                return BadRequest();
            }
            catch(ItemNotFoundException)
            {
                return NotFound();
            }
			
			return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}
		public IActionResult OnPostUnfollowCommunity(string CommunityName, Guid communityId)
		{
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _communityContainer.UnfollowCommunity(communityId, userId);
            }
            catch (AccessException)
            {
                return BadRequest();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("/BrowseCommunity", new { CommunityName = CommunityName });
		}

	}
}
