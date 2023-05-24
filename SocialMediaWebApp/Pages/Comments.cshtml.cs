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
        public IActionResult OnPostUpvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			Postid = PostId;

            
            var post = _postContainer.LoadPostById(Postid);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                post.Upvote();
                post.AddUpvotedUserId(userId);
		
				_postContainer.UpdatePostScore(post, userId, direction);
				return RedirectToPage("/Comments", new { PostId });
            }

        }

        public IActionResult OnPostRemoveUpvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			Postid = PostId;


			var post = _postContainer.LoadPostById(Postid);
			if (post == null)
			{
				return NotFound();
			}
			else
			{
				post.RemoveUpvote();
                post.RemoveUpvotedUserId(userId);

				_postContainer.UpdatePostScore(post, userId, direction);
				return RedirectToPage("/Comments", new { PostId });
			}
		}

        public IActionResult OnPostDownvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			Postid = PostId;

            var post = _postContainer.LoadPostById(Postid);
            if (post == null)
            {
                return NotFound();
            }
            post.Downvote();
            post.AddDownvotedUserId(userId);
			
			_postContainer.UpdatePostScore(post, userId, direction);
			return RedirectToPage("/Comments", new { PostId });

        }

        public IActionResult OnPostRemoveDownvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			Postid = PostId;

			var post = _postContainer.LoadPostById(Postid);
			if (post == null)
			{
				return NotFound();
			}
			post.RemoveDownvote();
            post.RemoveDownvotedUserId(userId);

			_postContainer.UpdatePostScore(post, userId, direction);
			return RedirectToPage("/Comments", new { PostId });
		}



        public IActionResult OnPostUpvoteComment(Guid commentId, Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			Postid = PostId;


            var comment = _commentContainer.LoadCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Upvote();
            comment.AddUpvotedUserId(userId);

            _commentContainer.UpdateCommentScore(comment, userId, direction);
            return RedirectToPage("/Comments", new { PostId });
        }

        public IActionResult OnPostRemoveUpvoteComment(Guid commentId, Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			Postid = PostId;


			var comment = _commentContainer.LoadCommentById(commentId);
			if (comment == null)
			{
				return NotFound();
			}
			comment.RemoveUpvote();
			comment.RemoveUpvotedUserId(userId);

			_commentContainer.UpdateCommentScore(comment, userId, direction);
			return RedirectToPage("/Comments", new { PostId });
		}

        public IActionResult OnPostDownvoteComment(Guid commentId, Guid PostId, string direction)
        {

			var userId = Guid.Parse(User.FindFirst("UserId").Value);


			var comment = _commentContainer.LoadCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Downvote();
            comment.AddDownvotedUserId(userId);
			_commentContainer.UpdateCommentScore(comment, userId, direction);
			return RedirectToPage("/Comments", new { PostId });

        }

        public IActionResult OnPostRemoveDownvoteComment(Guid commentId, Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);


			var comment = _commentContainer.LoadCommentById(commentId);
			if (comment == null)
			{
				return NotFound();
			}
			comment.Removedownvote();
			comment.RemoveDownvotedUserId(userId);
			_commentContainer.UpdateCommentScore(comment, userId, direction);
			return RedirectToPage("/Comments", new { PostId });
		}

        public IActionResult OnPostDeleteComment(Guid PostId, Guid CommentId)
        {

            _commentContainer.DeleteComment(CommentId);
            return RedirectToPage("/Comments", new { PostId });
        }
        

    }
}
