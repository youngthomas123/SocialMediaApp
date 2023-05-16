using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.DataAccess;
using System.Collections.Specialized;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class BrowseUserModel : PageModel
    {

        private readonly IUserContainer _userContainer;


        public BrowseUserModel(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        public  ProfileDto Profile { get; set; }

        public string ProfilePicture { get; set; }

        

        

        public void OnGet(string UserName)
        {
            var BrowseUserId =  Guid.Parse(_userContainer.GetUserId(UserName));

            

            Profile = _userContainer.GetProfileDto(BrowseUserId);

           


            if (Profile.ProfilePic != null)
            {
                string base64Image = Convert.ToBase64String(Profile.ProfilePic);
                ProfilePicture = $"data:image/png;base64,{base64Image}";
            }


            
        }

        public IActionResult OnPostAddFriend(Guid FriendId, string BrowseUserName)             // FriendId is the BrowseUserId
        {
            var userId = User.FindFirst("UserId").Value;

            _userContainer.AddFriend(new Guid(userId), FriendId);

            return RedirectToPage("/BrowseUser", new { UserName = BrowseUserName });
        }

        public IActionResult OnPostRemoveFriend(Guid FriendId, string BrowseUserName)
        {
            var userId = User.FindFirst("UserId").Value;

            _userContainer.RemoveFriend(new Guid(userId), FriendId);

            return RedirectToPage("/BrowseUser", new { UserName = BrowseUserName });
        }
    }
}
