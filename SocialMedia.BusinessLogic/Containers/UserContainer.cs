using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class UserContainer : IUserContainer
    {
        private readonly IUserDataAccess _userDataAcess;

        public UserContainer(IUserDataAccess userDataAcess)
        {
            _userDataAcess = userDataAcess;
        }

        public void SaveUser(User user)
        {
            _userDataAcess.SaveUser(user);
        }
    }
}
