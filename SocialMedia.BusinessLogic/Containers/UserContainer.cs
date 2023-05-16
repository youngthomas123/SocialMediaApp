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
        private readonly IUserFriendsDataAccess _userFriendsDataAccess;

        public UserContainer(IUserDataAccess userDataAcess, IPasswordHelper passwordHelper, IAuthenticationSystem authenticationSystem, IProfileDataAccess profileDataAccess, IUserFriendsDataAccess userFriendsDataAccess)
        {
            _userDataAccess = userDataAcess;
            _passwordHelper = passwordHelper;
            _authenticationSystem = authenticationSystem;
            _profileDataAccess = profileDataAccess;
			_userFriendsDataAccess = userFriendsDataAccess;

		}

        public void SaveUser(User user)
        {
            var salt = _passwordHelper.GetSalt();
            user.SetSalt(salt);
            var hashedPassword = _passwordHelper.GetHashedPassword(user.Password, salt);
            user.SetHashedPassword(hashedPassword);
            _userDataAccess.SaveUser(user);
            _profileDataAccess.CreateRecord(user.UserId, user.UserName);

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
            ProfileDto profile = _profileDataAccess.LoadProfileRecord(userId);

            var friends = _userFriendsDataAccess.GetUserFriends(userId);

            foreach (Guid friend in friends )
            {
                profile.Friends.Add(friend);

            }

            return profile;
        }

        public void AddFriend(Guid userId, Guid friendId)
        {
            _userFriendsDataAccess.CreateRecord(userId, friendId);
        }

        public void RemoveFriend(Guid userId, Guid friendId)
        {
            _userFriendsDataAccess.DeleteRecord(userId, friendId);  
        }

        public bool CheckIfUserIsFriends(Guid userId, Guid friendId)
        {
            bool check = _userFriendsDataAccess.CheckRecordExists(userId, friendId);
            return check;
        }
    }
}
