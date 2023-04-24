using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.RenderTree;

namespace SocialMediaWebApp.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IUserContainer _userContainer;

        [BindProperty]
        public LoginVM LoginData { get; set; }

        public LoginModel(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost() 
        {
            if(ModelState.IsValid)
            {
                bool isUserValid = _userContainer.ValidateCredentials(LoginData.UserName, LoginData.Password);

                if (isUserValid)
                {
                    var UserId = _userContainer.GetUserId(LoginData.UserName);

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim("UserName", LoginData.UserName),
                        new Claim("UserId", UserId)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                    return RedirectToPage("/Index");
                }
                else 
                {
                    TempData["InvalidLogin"] = "UserName or Password is incorrect";
                    
                }

            }
            return Page();
        }

    }
}
