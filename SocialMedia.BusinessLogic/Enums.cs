using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialMedia.BusinessLogic
{
	public enum MessageStatus
	{
		Read,
		Unread,
        Deleated,
        None
    }
    public enum ReportPostReasons
	{
        [Display(Name = "Offensive or Inappropriate Content")]
        OffensiveOrInappropriateContent,

        [Display(Name = "Spam or Advertising")]
        SpamOrAdvertising,

        [Display(Name = "Harassment or Bullying")]
        HarassmentOrBullying,

        [Display(Name = "Fake or Misleading Information")]
        FakeOrMisleadingInformation,

        [Display(Name = "Intellectual Property Infringement")]
        IntellectualPropertyInfringement,

        [Display(Name = "Personal Information Disclosure")]
        PersonalInformationDisclosure
    }

}
