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

        public Guid UserId { get;  set; }

        public string CommunityName { get;  set; }

        public string Description { get;  set; }

        public List<string>? Rules { get; set; }    // a list of rules

        public List<Guid>? FollowingUserIds { get; set; } // usernames

        public List<string>? PostIds { get; set; } // post Ids



    }
}
