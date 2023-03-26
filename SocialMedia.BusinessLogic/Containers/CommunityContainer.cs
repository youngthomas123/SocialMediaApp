using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommunityContainer
    {

        private readonly ICommunityDataAcess communityDataAcess;

        public CommunityContainer(ICommunityDataAcess dataAcess)
        {
            communityDataAcess = dataAcess;
        }





    }
}
