
using SocialMedia.BusinessLogic.Dto;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SocialMediaWebApp.ViewModels
{
	public class PostVM
	{

		public PostVM() { }



        [Required(ErrorMessage = "Please select a community.")]
        public string CommunityId { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public string Option { get; set; }

        [MaxLength(750)]
        public string? Body { get; set; }

       
        public string? ImageURl { get; set; }

     

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Option == "Text" && string.IsNullOrEmpty(Body))
        //    {
        //        yield return new ValidationResult("Please enter the body text.", new[] { nameof(Body) });
        //    }
        //    else if (Option == "Image" && string.IsNullOrEmpty(ImageURl))
        //    {
        //        yield return new ValidationResult("Please enter the image URL.", new[] { nameof(ImageURl) });
        //    }
        //}



    }
}
