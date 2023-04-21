using SocialMedia.BusinessLogic.Interfaces;
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
        private readonly IUserDataAccess _userDataAccess;
        private readonly IPasswordHelper _passwordHelper;

        public UserContainer(IUserDataAccess userDataAcess, IPasswordHelper passwordHelper)
        {
            _userDataAccess = userDataAcess;
            _passwordHelper = passwordHelper;
        }

        public void SaveUser(User user)
        {
            var salt = _passwordHelper.GetSalt();
            user.SetSalt(salt);
            var hashedPassword = _passwordHelper.GetHashedPassword(user.Password, salt);
            user.SetHashedPassword(hashedPassword);
            _userDataAccess.SaveUser(user);
        }
    }
}
