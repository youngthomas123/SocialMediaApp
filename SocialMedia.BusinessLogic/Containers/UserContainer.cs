using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        private readonly IPostDataAccess _postDataAccess;
        private readonly ICommentDataAccess _commentDataAccess;
        private readonly IMessageDataAccess _messageDataAccess;
        private readonly ICommunityMembersDataAccess _communityMembersDataAccess;
        private readonly ICommunityModeratorsDataAccess _communityModeratorsDataAccess;
        private readonly ICommunityDataAccess _communityDataAccess;
    
        public UserContainer(IUserDataAccess userDataAcess, IPasswordHelper passwordHelper, IAuthenticationSystem authenticationSystem, IProfileDataAccess profileDataAccess, IUserFriendsDataAccess userFriendsDataAccess, IPostDataAccess postDataAccess, ICommentDataAccess commentDataAccess, IMessageDataAccess messageDataAccess, ICommunityMembersDataAccess communityMembersDataAccess, ICommunityModeratorsDataAccess communityModeratorsDataAccess, ICommunityDataAccess communityDataAccess)
        {
            _userDataAccess = userDataAcess;
            _passwordHelper = passwordHelper;
            _authenticationSystem = authenticationSystem;
            _profileDataAccess = profileDataAccess;
			_userFriendsDataAccess = userFriendsDataAccess;
            _postDataAccess = postDataAccess;
            _commentDataAccess = commentDataAccess;
            _messageDataAccess = messageDataAccess;
            _communityMembersDataAccess = communityMembersDataAccess;
            _communityModeratorsDataAccess = communityModeratorsDataAccess;
            _communityDataAccess = communityDataAccess;

        }


        public void CreateAndSaveSignedUpUser(string username, string password, string email)
        {
            bool isUserNameUnique = CheckUserName(username);

          

            if(isUserNameUnique == true)
            {
                User user = new RegularUser(username, password, email);
                SaveUser (user);
            }
            else
            {
                throw new UserCreationException();
            }


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
            var salt = _userDataAccess.GetSalt(username);
            var passwordFromDataBase = _userDataAccess.GetPassword(username);
            bool isValid = _authenticationSystem.ValidateCredentials(username, password, salt, passwordFromDataBase);
            return isValid;
        }
        public string GetUserId(string username)
        {
            var doesUsernameExist = _userDataAccess.DoesUsernameExist(username);

            if(doesUsernameExist == true)
            {
                var userId = _userDataAccess.GetUserId(username);

                return userId;
            }
            else
            {
                throw new ItemNotFoundException();
            }
           
        }

        public string GetUserName(Guid userId)
        {
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

            if (doesUserIdExist == true)
            {
                var username = _userDataAccess.GetUserName(userId);

                return username;
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public List<string[]>SearchUser(string query)
        {
            var namesAndIds = _userDataAccess.SearchUserNameAndId(query);

           

            return namesAndIds;


        }
        public void UpdateUserProfileData(Guid userId, string username, string bio, string gender, byte[] picture, string location)
        {
            // check if userId exists and bio is less than 200 characters
            bool doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);
           


            if (doesUserIdExist == true)
            {
                if(bio != null && gender!=null && location!=null)
                {
                    if (bio.Length <= 200 && gender.Length <= 20 && location.Length <= 50)
                    {
                        _profileDataAccess.UpdateRecord(userId, username, bio, gender, picture, location);
                    }
                    else
                    {
                        throw new InvalidInputException();
                    }
                }
                else
                {
                    throw new InvalidInputException();
                }
               
            }
            else
            {
                throw new ItemNotFoundException("UserId does not exist");
            }

           
        }

        public void UpdateUserProfileData(Guid userId, string username, string bio, string gender, string location)
        {
            bool doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);
            
            if (doesUserIdExist == true)
            {
                if(bio != null && gender!=null && location!=null)
                {
                    if (bio.Length <= 200 && gender.Length <= 20 && location.Length <= 50)
                    {
                        _profileDataAccess.UpdateRecord(userId, username, bio, gender, location);
                    }
                    else
                    {
                        throw new InvalidInputException();
                    }
                }
                else
                {
                    throw new InvalidInputException();
                }
                
            }
            else
            {
                throw new ItemNotFoundException("UserId does not exist");
            }

           
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

            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

            if(doesUserIdExist == true)
            {
                ProfileDto profile = _profileDataAccess.LoadProfileRecord(userId);

                var friends = _userFriendsDataAccess.GetUserFriends(userId);

                foreach (Guid friend in friends)
                {
                    profile.Friends.Add(friend);

                }

                return profile;
            }
            else
            {
                throw new ItemNotFoundException();
            }
           
        }

        public void AddFriend(Guid userId, Guid friendId)
        {
            var isUserAlreadyFriends = CheckIfUserIsFriends(userId, friendId);
            if(isUserAlreadyFriends == false)
            {
                _userFriendsDataAccess.CreateRecord(userId, friendId);
            }
            else
            {
                throw new AccessException("The user is already friends, hence cannot add friend");
            }
           

        }

        public void RemoveFriend(Guid userId, Guid friendId)
        {

            var isUserfriends = CheckIfUserIsFriends(userId, friendId);

            if(isUserfriends == true) 
            {
                _userFriendsDataAccess.DeleteRecord(userId, friendId);
            }
            else
            {
                throw new AccessException("The user is not friends, hence cannot remove friend");
            }
             
        }

        public bool CheckIfUserIsFriends(Guid userId, Guid friendId)
        {

            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);
            var doesFriendIdExist = _userDataAccess.DoesUserIdExist(friendId);

            if(doesUserIdExist == true && doesFriendIdExist == true)
            {
                bool check = _userFriendsDataAccess.CheckRecordExists(userId, friendId);
                return check;
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public User GetUserByName(string username)
        {
            var doesUsernameExist = _userDataAccess.DoesUsernameExist(username);

            if(doesUsernameExist == true)
            {
                var User = _userDataAccess.LoadUser(username);

                var profile = GetProfileDto(User.UserId);

                User.UpdateProfile(profile.Bio, profile.Gender, profile.Location, profile.ProfilePic);


                var userCreatedPosts = _postDataAccess.LoadPostsByUser(User.UserId);

                foreach (var post in userCreatedPosts)
                {
                    User.AddToUserCreatedPosts(post);
                }

                var userCreatedComments = _commentDataAccess.LoadCommentsByUser(User.UserId);

                foreach (var comment in userCreatedComments)
                {
                    User.AddToUserCreatedComments(comment);
                }

                var userFollowingCommunities = _communityMembersDataAccess.LoadCommunityIdsByMember(User.UserId);

                foreach(var communityId in userFollowingCommunities)
                {
                    var CommunityName = _communityDataAccess.GetCommunityName(communityId);
                    User.AddToUserFollowingCommunities(CommunityName);
                }

                var userModeratingCommunities = _communityModeratorsDataAccess.LoadCommunityIdsByUser(User.UserId);

                foreach(var communityId in userModeratingCommunities)
                {
                    var CommunityName = _communityDataAccess.GetCommunityName(communityId);
                    User.AddToUserModeratingCommunities(CommunityName);
                }

                var ReceivedMessages = _messageDataAccess.UserReceivedMessages(User.UserId);
                foreach (var message in ReceivedMessages)
                {
                    User.AddToReceivedMessages(message);
                }

                var UserFriends = _userFriendsDataAccess.GetUserFriends(User.UserId);
                foreach (var friendId in UserFriends)
                {
                    var FriendName = _userDataAccess.GetUserName(friendId);
                    User.AddToUserFriends(FriendName);
                }

                if(User is PremiumUser premiumUser)
                {
                    var UserCreatedCommunities = _communityDataAccess.GetCommunityNamesCreatedByUser(User.UserId);

                    foreach(var communityName in UserCreatedCommunities)
                    {
                        premiumUser.AddToUserCreatedCommunities(communityName);
                    }
                    
                }

                return User;
            }
            else
            {
                throw new ItemNotFoundException("Username Invalid");
            }


        }
        public List<string>GetAllUsernames()
        {
            var unsernames = _userDataAccess.GetUserNames();
            return unsernames;
        }
       
    }
}
