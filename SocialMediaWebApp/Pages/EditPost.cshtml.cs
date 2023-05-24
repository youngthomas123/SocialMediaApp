using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.DataAccess;
using SocialMediaWebApp.ViewModels;
using System.Reflection;

namespace SocialMediaWebApp.Pages
{
   
    [Authorize]
    public class EditPostModel : PageModel
    {
        private readonly IPostContainer _postContainer;


        public bool IsPostIdValid { get; set; }
        public PostPageDto PostDto { get; set; }

        [BindProperty]
        public EditPostVM EditPostVM { get; set; }  

        public EditPostModel(IPostContainer postContainer)
        {
            _postContainer = postContainer;
        }

        public void OnGet(Guid? PostId)
        {
            if(PostId.HasValue)
            {

                EditPostVM = new EditPostVM();

                var userId = Guid.Parse(User.FindFirst("UserId").Value);

                try
                {
                    PostDto = _postContainer.GetPostPageDtoById(PostId.Value, userId);
                    EditPostVM.Title = PostDto.Title;

                    EditPostVM.Body = PostDto.Body;

                    EditPostVM.ImageUrl = PostDto.ImageUrl;

                    IsPostIdValid = true;

                }
                catch(ItemNotFoundException ex)
                {
                    TempData["Error"]  = ex.Message;
                    IsPostIdValid = false;
                   
                }
                
            }
            else
            {
                TempData["Error"] = "No valid Post Id supplied";
                IsPostIdValid= false;
            }


        }

        public IActionResult OnPostEditPost(Guid PostId)
        {
            if(ModelState.IsValid)
            {
             
             

                if(EditPostVM.Body == null && EditPostVM.ImageUrl != null )
                {
                    _postContainer.UpdatePost(PostId, EditPostVM.Title, null, EditPostVM.ImageUrl);
                    TempData["EditStatus"] = "Post edited successfully";
                }
                else if (EditPostVM.Body != null  && EditPostVM.ImageUrl == null )
                {
                    _postContainer.UpdatePost(PostId, EditPostVM.Title, EditPostVM.Body, null);
                    TempData["EditStatus"] = "Post edited successfully";
                }
                else
                {
                    TempData["EditStatus"] = "Failed to edit post";
                }
                
            }
           

            return RedirectToPage("/EditPost", new { PostId });
        }
    }
}
