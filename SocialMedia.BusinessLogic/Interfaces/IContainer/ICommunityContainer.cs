using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IContainer
{
    public interface ICommunityContainer
    {
        List<CommunityFullDto> LoadCompleteCommunityDtos();

        List<string> LoadCommunityNames();

        List<CommunityIdentityDto> LoadCommunityIdentityDtos();

        void SaveRawCommunity(Community community);

        string GetCommunityId(string communityName);

        List<string> GetCommunityRules(Guid communityId);

        List<string[]> SearchCommunity(string query);

        CommunityFullDto LoadCompleteCommunityDto(string communityName);

        void FollowCommunity(Guid communityId, Guid userId);

        void UnfollowCommunity(Guid communityId, Guid userId);

        bool IsCommunityNameUnique(string communityName);

        void CreateAndSaveCommunity(Guid userId, string communityName, string description);


    }
}
