using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMediaWebApp.ViewModels;
using System.Text.Json;

namespace SocialMediaWebApp.Pages
{
    [Authorize]
    public class SearchModel : PageModel
    {

        private readonly IUserContainer _userContainer;
        private readonly ICommunityContainer _communityContainer;

        public SearchModel(IUserContainer userContainer, ICommunityContainer communityContainer)
        {
            _userContainer = userContainer;
            _communityContainer = communityContainer;

        }
        [BindProperty]
        public SearchVM SearchVM { get; set; }  
        public List<string[]>SearchedUserResults { get; set; }

        public List<string[]>SearchedCommunityResults { get; set; }
      
		public void OnGet()
		{
			if (TempData.TryGetValue("SearchedUserResultsJson", out var searchedUserResultsJsonObj) && searchedUserResultsJsonObj is string searchedUserResultsJson)
			{
				SearchedUserResults = JsonSerializer.Deserialize<List<string[]>>(searchedUserResultsJson);
			}

			if (TempData.TryGetValue("SearchedCommunityResultsJson", out var searchedCommunityResultsJsonObj) && searchedCommunityResultsJsonObj is string searchedCommunityResultsJson)
			{
				SearchedCommunityResults = JsonSerializer.Deserialize<List<string[]>>(searchedCommunityResultsJson);
			}
		}

		public IActionResult OnPostSearchUser()
        {

         
            SearchedUserResults =  _userContainer.SearchUser(SearchVM.SeachUser);

			if (SearchedUserResults == null)
			{
				SearchedUserResults = new List<string[]>(); 
			}

			string searchedUserResultsJson = JsonSerializer.Serialize(SearchedUserResults);

			TempData["SearchedUserResultsJson"] = searchedUserResultsJson;



			return RedirectToPage("/Search");
        }
        public IActionResult OnPostSearchCommunity()
        {

			SearchedCommunityResults = _communityContainer.SearchCommunity(SearchVM.SearchCommunity);

			if (SearchedCommunityResults == null)
			{
				SearchedCommunityResults = new List<string[]>();
			}

			string searchedCommunityResultsJson = JsonSerializer.Serialize(SearchedCommunityResults);

			TempData["SearchedCommunityResultsJson"] = searchedCommunityResultsJson;



			return RedirectToPage();
        }
    }
}
