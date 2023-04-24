using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;

namespace SocialMediaWebApp.Pages
{
    public class SignupModel : PageModel
    {
        private readonly IUserContainer _userContainer;

        
        [BindProperty]
        public SignUpVM SignUpData { get; set; }

     

        public SignupModel(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                bool isUserNameUnique = _userContainer.CheckUserName(SignUpData.UserName);

                if(isUserNameUnique ==true)
                {
                    TempData["SignInMessage"] = "Successfully account created.";
                    var user = new User(SignUpData.UserName, SignUpData.Password, SignUpData.Email);
                    _userContainer.SaveUser(user);
                }
                else
                {
                    TempData["SignInMessage"] = "Username is not available.";
                }
               
            }
           
            return Page();
        }
    }
}
