using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebApp.ViewModels
{
	public class ReportCommentVM
	{
		public ReportCommentVM() { }

		[Required]
		public int ReasonId { get; set; }
	}
}
