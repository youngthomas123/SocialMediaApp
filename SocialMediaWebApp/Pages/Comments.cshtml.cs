using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;

namespace SocialMediaWebApp.Pages
{
    public class CommentsModel : PageModel
    {

        public string PostId { get; set; }

        public PostPageDto Post { get; set; }

        [BindProperty]
        public CommentVM CommentData { get; set; }

        private readonly IPostContainer _postContainer;
        private readonly ICommentContainer _commentContainer;

        public CommentsModel(IPostContainer postContainer, ICommentContainer commentContainer ) 
        {
            _postContainer = postContainer;
            _commentContainer = commentContainer;

            Post = new PostPageDto();
        }


        
        public void OnGet()
        {

            int x = 0;
            PostId = Request.Cookies["PostId"];

            Post = _postContainer.GetPostPageDtoById(new Guid(PostId));
        }
        public IActionResult OnPostAddComment()
        {
            //if (ModelState.IsValid)
            //{
            //    _commentContainer.AddComment(PostId, CommentData);
            //    return RedirectToPage();
            //}
            return Page();
        }

        
    }
}
