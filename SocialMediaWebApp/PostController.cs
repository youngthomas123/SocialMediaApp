using Microsoft.AspNetCore.Mvc;
using SocialMedia.BusinessLogic.Interfaces.IContainer;

namespace SocialMediaWebApp
{
    public class PostController : Controller
    {
        private readonly IPostContainer _postContainer;

        public PostController(IPostContainer postContainer) 
        {
            _postContainer = postContainer;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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
				var userId = Guid.Parse(User.FindFirst("UserId").Value);
				_postContainer.UpdatePost(post);
                return RedirectToPage("/Index");
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
			var userId = Guid.Parse(User.FindFirst("UserId").Value);
			_postContainer.UpdatePost(post);
            return RedirectToPage("/Index");
        }

    }
}
