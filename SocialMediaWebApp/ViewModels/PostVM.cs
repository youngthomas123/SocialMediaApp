using SocialMedia.BusinessLogic.Dto;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SocialMediaWebApp.ViewModels
{
	public class PostVM
	{

		public PostVM() { }


		[Required]
		public string Title { get; set; }

	
		public string? Body { get; set; }


        [Required]
        public string CommunityId { get; set; }

		
		public string? ImageURl { get; set; }



	}
}
