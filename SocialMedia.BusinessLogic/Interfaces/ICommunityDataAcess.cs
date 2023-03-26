using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface ICommunityDataAcess
    {
        void SaveCommunity();

        void UpdateCommunity(Community community);

        List<Community> LoadCommunity();

        void DeleteCommunity(Guid id);

    }
}
