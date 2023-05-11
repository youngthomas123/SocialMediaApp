using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Specialized;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class BrowseUserModel : PageModel
    {
       
        public string Username { get; set; }

        public void OnGet(string UserName)
        {
            Username = UserName;
        }
    }
}
