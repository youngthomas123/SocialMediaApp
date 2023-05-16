﻿using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IContainer
{
    public interface IUserContainer
    {
        void SaveUser(User user);

        bool CheckUserName(string username);

        bool ValidateCredentials(string username, string password);

        string GetUserId(string username);

        List<string[]> SearchUser(string query);

        void UpdateUserProfileData(Guid userId, string username, string bio, string gender, byte[] picture, string location);

        void UpdateProfilePicture(Guid userId, byte[] picture);

        byte[] GetProfilePicture(Guid userId);

        ProfileDto GetProfileDto(Guid userId);

        void UpdateUserProfileData(Guid userId, string username, string bio, string gender, string location);

        void AddFriend(Guid userId, Guid friendId);

        void RemoveFriend(Guid userId, Guid friendId);

        bool CheckIfUserIsFriends(Guid userId, Guid friendId);



    }
}
