﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using System.Net;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IPostContainer _postContainer;
        public List<PostPageDto> postDtos; 



        public IndexModel(ILogger<IndexModel> logger, IPostContainer postContainer)
        {
            _logger = logger;
            _postContainer = postContainer;
           
        }
        
        public void OnGet()
        {
            postDtos = _postContainer.GetPostPageDtos();

            Response.Cookies.Delete("PostId");
            Response.Cookies.Delete("BrowseUser");
            Response.Cookies.Delete("BrowseCommunity");

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
        public IActionResult OnPostViewComments(Guid postId)
        {
            Response.Cookies.Append("PostId", postId.ToString());

            return RedirectToPage("/Comments");



        }
    }
}
