using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface IUserDataAcess
    {
        void SaveUser(User user);

        void UpdateUser(User user);

        List<User> LoadUser();

        void DeleteUser(Guid id);



    }
}
