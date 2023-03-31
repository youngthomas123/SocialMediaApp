using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;

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
    }
}