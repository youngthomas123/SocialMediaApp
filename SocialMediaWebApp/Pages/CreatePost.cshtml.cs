using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMediaWebApp.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaWebApp.Pages
{
    public class PostModel : PageModel
    {
		[BindProperty]
		public PostVM PostData { get; set; }
        [BindProperty]
        public int SelectedValue { get; set; }
        public List<SelectListItem> Values { get; set; }

        private ICommunityDataAcess _communityDataAcess;
        public PostModel(ICommunityDataAcess communityDataAcess)
        {
            _communityDataAcess = communityDataAcess;
        }

        public void OnGet()
        {
            var communities = _communityDataAcess.LoadCommunity();
            Values = new();
            foreach (var item in communities)
            {
                Values.Add(new(item.Name, item.CommunityId.ToString()));
            }
        }

        public void OnPost ()
        {
            if (ModelState.IsValid)
            {
                
            }
        }
    }
}
