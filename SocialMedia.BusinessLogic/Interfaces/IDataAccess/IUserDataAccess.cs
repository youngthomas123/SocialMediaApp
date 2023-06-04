using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface IUserDataAccess
    {
        void SaveUser(User user);

        void UpdateUserById(User user);

        List<User> LoadUser();

        void DeleteUserByUserName(string username);

        void DeleteUserById(Guid UserId);

        List<string> GetUserNames();

        string? GetSalt(string username);

        string? GetPassword(string username);

        string? GetUserId(string username);

        string GetUserName(Guid UserId);

        List<string[]>? SearchUserNameAndId(string searchquery);

        bool DoesUserIdExist(Guid userId);

        bool DoesUsernameExist(string username);

        User? LoadUser(string username);



    }
}
