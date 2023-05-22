using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Custom_exception;
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
               
                try
                {
                    _userContainer.CreateAndSaveSignedUpUser(SignUpData.UserName, SignUpData.Password, SignUpData.Email);
                    TempData["SignInMessage"] = "User Created successfully";
                }
                catch (UserCreationException ex)
                {
                    TempData["SignInMessage"] = ex.Message;
                    
                }

            }
           
            return Page();
        }
    }
}
