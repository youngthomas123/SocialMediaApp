using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Dto;
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

        public string ProfilePicture { get; set; }  

        public ProfileDto Profile { get; set; }

        public void OnGet()
        {
           
            var UserId = User.FindFirst("UserId").Value;

            Profile = _userContainer.GetProfileDto(new Guid(UserId));


            if (Profile.ProfilePic != null)
            {
                string base64Image = Convert.ToBase64String(Profile.ProfilePic);
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

        //public IActionResult OnPostEditProfile()
        //{
            
        //}
    }
}
