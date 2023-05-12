using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public string UserName { get; set; }

        public void OnGet()
        {
            UserName = User.FindFirst("UserName").Value;
            
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
