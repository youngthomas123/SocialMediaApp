using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class EditProfileModel : PageModel
    {

        private readonly IUserContainer _userContainer;

        public EditProfileModel(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }
        [BindProperty]
        public EditProfileVM EditProfileVM { get; set; }

        public void OnGet()
        {

            EditProfileVM = new EditProfileVM();

            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            var CurrentData = _userContainer.GetProfileDto(userId);


            EditProfileVM.Gender = CurrentData.Gender;
            EditProfileVM.Location = CurrentData.Location;
            EditProfileVM.Bio = CurrentData.Bio;


        }
        public IActionResult OnPost() 
        {
            if(ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("UserId").Value);
                var userName = (User.FindFirst("UserName").Value);


                if (EditProfileVM.ProfilePic != null)
                {
                    byte[] profilePicData;
                    using (var memoryStream = new MemoryStream())
                    {
                        EditProfileVM.ProfilePic.CopyTo(memoryStream);
                        profilePicData = memoryStream.ToArray();

                    }


                    try
                    {
                        _userContainer.UpdateUserProfileData(userId, userName, EditProfileVM.Bio, EditProfileVM.Gender, profilePicData, EditProfileVM.Location);
                        return RedirectToPage();
                    }
                    catch(InvalidInputException ex)
                    {
                        TempData["Error"] = ex.Message;
                       
                    }
                    catch (ItemNotFoundException ex)
                    {
                        TempData["Error"] = ex.Message;
                        
                    }
    
                }
                else
                {
                    try
                    {
                        _userContainer.UpdateUserProfileData(userId, userName, EditProfileVM.Bio, EditProfileVM.Gender, EditProfileVM.Location);
                        return RedirectToPage();
                    }
                    catch (InvalidInputException ex)
                    {
                        TempData["Error"] = ex.Message;
                       
                    }
                    catch (ItemNotFoundException ex)
                    {
                        TempData["Error"] = ex.Message;
                        
                    }

                }
            }
            return RedirectToPage();



        }
    }
}
