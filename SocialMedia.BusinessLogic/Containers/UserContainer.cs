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
        private readonly IAuthenticationSystem _authenticationSystem;

        public UserContainer(IUserDataAccess userDataAcess, IPasswordHelper passwordHelper, IAuthenticationSystem authenticationSystem)
        {
            _userDataAccess = userDataAcess;
            _passwordHelper = passwordHelper;
            _authenticationSystem = authenticationSystem;
        }

        public void SaveUser(User user)
        {
            var salt = _passwordHelper.GetSalt();
            user.SetSalt(salt);
            var hashedPassword = _passwordHelper.GetHashedPassword(user.Password, salt);
            user.SetHashedPassword(hashedPassword);
            _userDataAccess.SaveUser(user);

        }
        public bool CheckUserName(string username)
        {
            bool isUserNameUnique = true;

            foreach (string _ in _userDataAccess.GetUserNames())
            {
                if (username == _)
                {
                    isUserNameUnique = false;
                    break;
                }
                
            }
            return isUserNameUnique;
        }
        public bool ValidateCredentials(string username, string password)
        {
            bool isValid = _authenticationSystem.ValidateCredentials(username, password);
            return isValid;
        }
        public string GetUserId(string username)
        {
            var userId = _userDataAccess.GetUserId(username);

            return userId;
        }

        public List<string[]>SearchUser(string query)
        {
            var namesAndIds = _userDataAccess.SearchUserNameAndId(query);


            return namesAndIds;


        }
    }
}
