using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.DataAccess;
using SocialMediaWebApp.ViewModels;
using System.Reflection;

namespace SocialMediaWebApp.Pages
{
    public class EditPostModel : PageModel
    {
        private readonly IPostContainer _postContainer;


        
        public PostPageDto PostDto { get; set; }

        [BindProperty]
        public EditPostVM EditPostVM { get; set; }  

        public EditPostModel(IPostContainer postContainer)
        {
            _postContainer = postContainer;
        }

        public void OnGet(Guid PostId)
        {
            EditPostVM = new EditPostVM();

            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            PostDto = _postContainer.GetPostPageDtoById(PostId, userId);

            

            EditPostVM.Title = PostDto.Title;

            EditPostVM.Body = PostDto.Body;

            EditPostVM.ImageUrl = PostDto.ImageUrl;
        }

        public IActionResult OnPostEditPost(Guid PostId)
        {
            if(ModelState.IsValid)
            {
             
             

                if(EditPostVM.Body == null && EditPostVM.ImageUrl != null )
                {
                    _postContainer.UpdatePost(PostId, EditPostVM.Title, null, EditPostVM.ImageUrl);
                }
                else if (EditPostVM.Body != null  && EditPostVM.ImageUrl == null )
                {
                    _postContainer.UpdatePost(PostId, EditPostVM.Title, EditPostVM.Body, null);
                }
                
                
            }
           

            return RedirectToPage("/EditPost", new { PostId });
        }
    }
}
