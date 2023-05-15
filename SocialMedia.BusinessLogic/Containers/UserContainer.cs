using SocialMedia.BusinessLogic.Dto;
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
        private readonly IProfileDataAccess _profileDataAccess;

        public UserContainer(IUserDataAccess userDataAcess, IPasswordHelper passwordHelper, IAuthenticationSystem authenticationSystem, IProfileDataAccess profileDataAccess)
        {
            _userDataAccess = userDataAcess;
            _passwordHelper = passwordHelper;
            _authenticationSystem = authenticationSystem;
            _profileDataAccess = profileDataAccess;
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
        public void UpdateUserProfileData(Guid userId, string username, string bio, string gender, byte[] picture, string location)
        {
            _profileDataAccess.UpdateRecord(userId, username, bio, gender, picture, location);
        }

        public void UpdateUserProfileData(Guid userId, string username, string bio, string gender, string location)
        {
            _profileDataAccess.UpdateRecord(userId, username, bio, gender,  location);
        }

        public void UpdateProfilePicture(Guid userId, byte[] picture)
        {
            _profileDataAccess.UpdateProfilePicture(userId, picture);
        }

        public byte[] GetProfilePicture(Guid userId)
        {
           var pic =  _profileDataAccess.GetProfilePicture(userId);

            return pic;
            
        }

        public ProfileDto GetProfileDto(Guid userId)
        {
            var profile = _profileDataAccess.LoadProfileRecord(userId);

            return profile;
        }
    }
}
