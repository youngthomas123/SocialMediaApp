using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Newtonsoft.Json.Linq;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMediaWebApp.ViewModels;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class PostModel : PageModel
    {

        
        [BindProperty]
		public PostVM PostData { get; set; }

        public List<SelectListItem> CommunityIdentities { get; set; }

        private ICommunityContainer _communityContainer { get; set; }

        private IPostContainer _postContainer { get; set; }
        public PostModel(ICommunityContainer communityContainer, IPostContainer postContainer)
        {
            _communityContainer = communityContainer;

            _postContainer = postContainer;

            CommunityIdentities = new List<SelectListItem>();

            


        }

        public void OnGet()
        {
            var CommmunityIdentityDtos = _communityContainer.LoadCommunityIdentityDtos();

            foreach (var Community in CommmunityIdentityDtos)
            {
                CommunityIdentities.Add(new(Community.CommunityName, Community.CommunityId.ToString()));
            }

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

               
                var userId = Guid.Parse(User.FindFirst("UserId").Value);



                if (PostData.Option == "Text" && PostData.Body != null)
                {
                    Post post = new Post(userId, PostData.Title, PostData.Body, null, new Guid(PostData.CommunityId));
                    _postContainer.SavePost(post);
                    TempData["PostStatus"] = "Post created successfully";
                }
                else if (PostData.Option == "Image" && PostData.ImageURl != null)
                {
                    Post post = new Post(userId, PostData.Title, null, PostData.ImageURl, new Guid(PostData.CommunityId));
                    _postContainer.SavePost(post);
                    TempData["PostStatus"] = "Post created successfully";
                }
                else
                {
                    
                    TempData["PostStatus"] = "Failed to create post (Text or Image not supplied)";
                }
            }
            else
            {
                

                TempData["PostStatus"] = "Failed to create post";
            }

            return RedirectToPage();

        }

            
            
    }

       
}


