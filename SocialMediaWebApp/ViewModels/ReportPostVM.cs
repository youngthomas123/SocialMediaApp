using SocialMedia.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
	public class ReportPostVM
	{
		public ReportPostVM() { }

		[Required]
		public int ReasonId { get; set; }
	}
}
