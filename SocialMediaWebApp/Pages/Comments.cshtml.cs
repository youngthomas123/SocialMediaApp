using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;
using System.Runtime.InteropServices;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class CommentsModel : PageModel
    {
       
        public string PostId { get; set; }

        public PostPageDto PostDtos { get; set; }

        public List <CommentPageDto> CommentDtos { get; set; }

        [BindProperty]
        public CommentVM CommentData { get; set; }

        private readonly IPostContainer _postContainer;
        private readonly ICommentContainer _commentContainer;

        public CommentsModel(IPostContainer postContainer, ICommentContainer commentContainer ) 
        {
            _postContainer = postContainer;
            _commentContainer = commentContainer;

            PostDtos = new PostPageDto();
            PostId = "00000000-0000-0000-0000-000000000000";
            
        }


        
        public void OnGet()
        {

            
            PostId = Request.Cookies["PostId"];

            PostDtos = _postContainer.GetPostPageDtoById(new Guid(PostId));
            CommentDtos = _commentContainer.GetCommentPageDtosInPost(new Guid(PostId));

        }
        public IActionResult OnPostAddComment()
        {
            
            if (ModelState.IsValid)
            {
                PostId = Request.Cookies["PostId"];
                var userId = Guid.Parse(User.FindFirst("UserId").Value);
                Comment comment = new Comment(userId, CommentData.Body, Guid.Parse(PostId));
                _commentContainer.AddComment(comment);
                
            }
            return RedirectToPage();
        }
        public IActionResult OnPostUpvote(Guid postId)
        {
            var post = _postContainer.LoadPostById(postId);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                post.upvote();
                _postContainer.UpdatePost(post);
                return RedirectToPage();
            }

        }

        public IActionResult OnPostDownvote(Guid postId)
        {
            var post = _postContainer.LoadPostById(postId);
            if (post == null)
            {
                return NotFound();
            }
            post.downvote();
            _postContainer.UpdatePost(post);
            return RedirectToPage();
        }
        public IActionResult OnPostUpvoteComment(Guid commentId)
        {
            var comment = _commentContainer.LoadCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Upvote();
            _commentContainer.UpDateComment(comment);
            return RedirectToPage();
        }

        public IActionResult OnPostDownvoteComment(Guid commentId)
        {
            var comment = _commentContainer.LoadCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Downvote();
            _commentContainer.UpDateComment(comment);
            return RedirectToPage();
        }

    }
}
