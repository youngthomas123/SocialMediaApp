using SocialMedia.BusinessLogic.Dto;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
	public class PostVM
	{

		public PostVM() { }


		[Required]
		public string Title { get; set; }

		[Required]
		public string Body { get; set; }




        [Required]
        public string CommunityId { get; set; }	

	}
}
