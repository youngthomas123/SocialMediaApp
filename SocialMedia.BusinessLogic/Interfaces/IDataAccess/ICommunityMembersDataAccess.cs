using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface ICommunityMembersDataAccess
    {
     
        
        void CreateMember(Guid communityId, Guid UserId);

        void DeleteMember(Guid communityId, Guid UserId);

        List<Guid> LoadUserIds(Guid CommunityId);

        void UpdateUserId(Guid communityId, Guid UserId);




    }
}
