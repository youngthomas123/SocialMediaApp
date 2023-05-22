using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface IAuthenticationSystem
    {
        bool ValidateCredentials(string username, string password, string? salt,string? passwordFromDataBase);
    }
}
