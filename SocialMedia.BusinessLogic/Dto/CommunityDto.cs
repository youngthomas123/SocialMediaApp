using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Dto
{
    public class CommunityDto
    {
        public CommunityDto() { }

        public DateTime DateCreated { get;  set; }

        public Guid CommunityId { get;  set; }

        public string Creator { get;  set; }

        public string CommunityName { get;  set; }

        public string Description { get;  set; }

        public List<string> Rules { get; set; }

        public List <string>MemberUsernames { get; set; }

        public List<string>PostId { get; set; }


        
    }
}
