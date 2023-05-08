using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class BrowseCommunityModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
