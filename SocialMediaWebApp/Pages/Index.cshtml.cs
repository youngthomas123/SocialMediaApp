using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces.IContainer;

namespace SocialMediaWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IPostContainer _postContainer;
        public List<Post> posts; 



        public IndexModel(ILogger<IndexModel> logger, IPostContainer postContainer)
        {
            _logger = logger;
            _postContainer = postContainer;

        }
       
        public void OnGet()
        {
            posts = _postContainer.LoadAllPosts();
        }

        public void OnPost()
        {


        }
        
        public IActionResult OnPostUpvote(Guid postId)
        {
            var post = _postContainer.LoadPostById(postId);
            if (post == null)
            {
                return NotFound();
            }
            post.upvote();
            _postContainer.UpdatePost(post);
            return RedirectToPage();
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
    }
}
