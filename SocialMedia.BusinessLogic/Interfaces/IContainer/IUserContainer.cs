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

	}
}
