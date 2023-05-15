using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IUserContainer _userContainer;

        public ProfileModel(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        public string UserName { get; set; }

        public string ProfilePicture { get; set; }

        public void OnGet()
        {
            UserName = User.FindFirst("UserName").Value;
            var UserId = User.FindFirst("UserId").Value;

            var ProfilePic = _userContainer.GetProfilePicture(new Guid(UserId));
            if(ProfilePic !=null)
            {
                string base64Image = Convert.ToBase64String(ProfilePic);
                ProfilePicture = $"data:image/png;base64,{base64Image}";
            }
          



        }
        public IActionResult OnPostLogout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Login");
        }
    }
}
