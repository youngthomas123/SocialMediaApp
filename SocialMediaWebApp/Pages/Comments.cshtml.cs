using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Custom_exception;
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

        public bool IsPostIdValid { get; set; }

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


        
        public void OnGet(Guid? PostId)
        {
			if(PostId.HasValue)
            {
                var userId = Guid.Parse(User.FindFirst("UserId").Value);

                try
                {
                
                    PostDto = _postContainer.GetPostPageDtoById(PostId.Value, userId);
                    CommentDtos = _commentContainer.GetCommentPageDtosInPost(PostId.Value, userId);
                    Postid = PostId.Value;
                    IsPostIdValid = true;
                }
                catch (ItemNotFoundException ex)
                {
                    TempData["Error"] = ex.Message;
                    IsPostIdValid = false;
                }

            }
            else
            {
                TempData["Error"] = "No valid Post Id supplied";
                IsPostIdValid = false;
            }

		}

       // post functionality
        public IActionResult OnPostUpvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.Upvote(PostId, direction, userId);
                return RedirectToPage("/Comments", new { PostId });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }

        }

        public IActionResult OnPostRemoveUpvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.RemoveUpvote(PostId, direction, userId);
                return RedirectToPage("/Comments", new { PostId });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }

        }

        public IActionResult OnPostDownvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);


            try
            {
                _postContainer.Downvote(PostId, direction, userId);
                return RedirectToPage("/Comments", new { PostId });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }

        }

        public IActionResult OnPostRemoveDownvotePost(Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.RemoveDownvote(PostId, direction, userId);
                return RedirectToPage("/Comments", new { PostId });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }

        public IActionResult OnPostDeletePost(Guid PostId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _postContainer.DeletePost(PostId, userId);
            }
            catch (AccessException)
            {
                return BadRequest();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }


            return RedirectToPage("/Index");
        }

        public IActionResult OnPostReportPost(Guid postId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            try
            {
                _postContainer.ReportPost(postId, userId);
            }
            catch (AccessException)
            {
                return BadRequest();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }



            return RedirectToPage("/Comments", new { postId });
        }


        // comment functionality

        public IActionResult OnPostAddComment(Guid PostId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            if (ModelState.IsValid)
            {

                Comment comment = new Comment(userId, CommentData.Body, PostId);

                try
                {
                    _commentContainer.AddComment(comment);
                }
                catch (InvalidInputException ex)
                {
                    TempData["Status"] = ex.Message;
                }


            }
            return RedirectToPage("/Comments", new { PostId });
        }

        public IActionResult OnPostUpvoteComment(Guid commentId, Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _commentContainer.Upvote(commentId, direction, userId);
              
                return RedirectToPage("/Comments", new { PostId });
            }
            catch(ItemNullException)
            {
                return NotFound();
            }
        }

        public IActionResult OnPostRemoveUpvoteComment(Guid commentId, Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _commentContainer.RemoveUpvote(commentId, direction, userId);
                return RedirectToPage("/Comments", new { PostId });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }

        public IActionResult OnPostDownvoteComment(Guid commentId, Guid PostId, string direction)
        {

			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _commentContainer.Downvote(commentId, direction, userId);
                return RedirectToPage("/Comments", new { PostId });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }
        }

        public IActionResult OnPostRemoveDownvoteComment(Guid commentId, Guid PostId, string direction)
        {
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _commentContainer.RemoveDownvote(commentId, direction, userId);
                return RedirectToPage("/Comments", new { PostId });
            }
            catch (ItemNullException)
            {
                return NotFound();
            }

        }

        public IActionResult OnPostDeleteComment(Guid PostId, Guid CommentId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            try
            {
                _commentContainer.DeleteComment(CommentId, userId);
            }
            catch(AccessException)
            {
                return BadRequest();    
            }
            catch(ItemNotFoundException)
            {
                return NotFound() ; 
            }
           
            return RedirectToPage("/Comments", new { PostId });
        }
        

    }
}
