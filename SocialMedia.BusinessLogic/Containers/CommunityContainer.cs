using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommunityContainer : ICommunityContainer
    {

        private readonly ICommunityDataAcess _communityDataAcess;

        public CommunityContainer(ICommunityDataAcess communityDataAcess)
        {
            _communityDataAcess = communityDataAcess;
        }

        public List<Community> LoadCommunity()
        {
             return _communityDataAcess.LoadCommunity();
        }
    }
}
