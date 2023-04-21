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

        //passwordHelper
        PasswordHelper passwordHelper = new PasswordHelper();


        [BindProperty]
        public SignUpVM SignUpData { get; set; }

        public SignupModel(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        //[BindProperty]
        //public string Username { get; set; }

        //[BindProperty]
        //public string Password { get; set; }

        //[BindProperty]
        //public string Email { get; set; }

        //public bool? SuccessfulSignup { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var salt=  passwordHelper.GetSalt();
                SignUpData.Password = passwordHelper.GetHashedPassword(SignUpData.Password, salt);

                var user = new User(SignUpData.UserName, SignUpData.Password, SignUpData.Email);
                _userContainer.SaveUser(user);
            }
           
            //var user = new User(Username, Password, Email);
            //_userContainer.SaveUser(user);

            //SuccessfulSignup = true;

            return Page();
        }
    }
}
