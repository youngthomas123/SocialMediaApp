using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMediaWebApp.ViewModels;

namespace SocialMediaWebApp.Pages
{
    public class PostModel : PageModel
    {
		[BindProperty]
		public PostVM PostData { get; set; }

     

        public void OnGet()
        {

        }

        public void OnPost ()
        {
            if (ModelState.IsValid)
            {
                
            }
        }
    }
}
