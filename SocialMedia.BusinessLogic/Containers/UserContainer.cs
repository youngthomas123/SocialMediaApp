using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class UserContainer 
    {
        private readonly IUserDataAcess userDataAcess;

        public UserContainer(IUserDataAcess dataAcess)
        {
            userDataAcess = dataAcess;
        }

    }
}
