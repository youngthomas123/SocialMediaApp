using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.DataAccess;
using System.Collections.Specialized;
using System.Linq;

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
        public Guid BrowseUserId { get; set; }

        public Guid LoggedInUser { get; set; }
        public  ProfileDto Profile { get; set; }

        public string ProfilePicture { get; set; }

        public bool isAlreadyFriend { get; set; }

        

        public void OnGet(string UserName)
        {
             BrowseUserId =  Guid.Parse(_userContainer.GetUserId(UserName));    // user whos profile is being viewed

            LoggedInUser = Guid.Parse(User.FindFirst("UserId").Value);                   // user who is logged in

            Profile = _userContainer.GetProfileDto(BrowseUserId);

            isAlreadyFriend= _userContainer.CheckIfUserIsFriends(LoggedInUser, BrowseUserId);        // is logged in user already friends with the browseUser


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
