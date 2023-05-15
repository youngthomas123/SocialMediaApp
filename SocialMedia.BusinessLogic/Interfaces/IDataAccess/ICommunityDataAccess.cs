using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface ICommunityDataAccess
    {
        void SaveCommunity(Community community);

        void UpdateCommunity(Community community);

        List<Community> LoadCommunitys();

        void DeleteCommunity(Guid id);

        List<string> GetCommunityNames();

        string GetCommunityId(string communityname);

        List<Array> GetCommunityNameAndId();

        string GetCommunityName(Guid CommunityId);

        List<string[]>? SearchCommunityAndId(string searchquery);

	}
}
