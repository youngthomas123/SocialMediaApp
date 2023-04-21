using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface IPasswordHelper
    {
        string GetHashedPassword(string rawPassword, string salt);

        string GetSalt();


    }
}
