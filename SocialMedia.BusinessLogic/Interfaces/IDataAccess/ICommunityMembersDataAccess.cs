using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface ICommunityMembersDataAccess
    {
        void CreateMember(Guid communityId, string member);
        void DeleteMember(Guid communityId, string member);
        List<string> LoadMembers(Guid CommunityId);
        void UpdateMember(Guid communityId, string member);


    }
}
