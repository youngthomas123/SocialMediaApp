using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.DataAccess;
using SocialMediaWebApp.ViewModels;
using System.Runtime.InteropServices;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class CommentsModel : PageModel
    {

        
		
        public Guid Postid { get; set; }

        public PostPageDto PostDto { get; set; }

        public List <CommentPageDto> CommentDtos { get; set; }

        [BindProperty]
        public CommentVM CommentData { get; set; }

        private readonly IPostContainer _postContainer;
        private readonly ICommentContainer _commentContainer;

        public CommentsModel(IPostContainer postContainer, ICommentContainer commentContainer ) 
        {
            _postContainer = postContainer;
            _commentContainer = commentContainer;

            PostDto = new PostPageDto();
            Postid = new Guid( "00000000-0000-0000-0000-000000000000");
            
        }


        
        public void OnGet(Guid PostId)
        {
			 Postid = PostId;
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			PostDto = _postContainer.GetPostPageDtoById(Postid , userId);
            CommentDtos = _commentContainer.GetCommentPageDtosInPost(Postid, userId);

			
		}
        public IActionResult OnPostAddComment(Guid PostId)
        {

            Postid = PostId;

            

            

            if (ModelState.IsValid)
            {
                
                var userId = Guid.Parse(User.FindFirst("UserId").Value);

                
                Comment comment = new Comment(userId, CommentData.Body, Postid);
                _commentContainer.AddComment(comment);


            }
            return RedirectToPage("/Comments", new { PostId });
        }
        public IActionResult OnPostUpvote(Guid PostId)
        {

            Postid = PostId;

            
            var post = _postContainer.LoadPostById(Postid);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                post.upvote();
				var userId = Guid.Parse(User.FindFirst("UserId").Value);
				_postContainer.UpdatePostScore(post, userId, "up");
				return RedirectToPage("/Comments", new { PostId });
            }

        }

        public IActionResult OnPostDownvote(Guid PostId)
        {
            Postid= PostId;

            var post = _postContainer.LoadPostById(Postid);
            if (post == null)
            {
                return NotFound();
            }
            post.downvote();
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			_postContainer.UpdatePostScore(post, userId, "down");
			return RedirectToPage("/Comments", new { PostId });
        }
        public IActionResult OnPostUpvoteComment(Guid commentId, Guid PostId)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			Postid = PostId;


            var comment = _commentContainer.LoadCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Upvote();
            _commentContainer.UpdateCommentScore(comment, userId, "up");
            return RedirectToPage("/Comments", new { PostId });
        }

        public IActionResult OnPostDownvoteComment(Guid commentId, Guid PostId)
        {

			var userId = Guid.Parse(User.FindFirst("UserId").Value);


			var comment = _commentContainer.LoadCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Downvote();
			_commentContainer.UpdateCommentScore(comment, userId, "down");
			return RedirectToPage("/Comments", new { PostId });
        }

    }
}
