using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Dto
{
    public class ProfileDto
    {
        public ProfileDto() { }
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Bio { get; set; }

        public string Gender { get; set; }  

        public Byte[] ProfilePic { get; set; }

        public string Location { get; set; }

    }
}
