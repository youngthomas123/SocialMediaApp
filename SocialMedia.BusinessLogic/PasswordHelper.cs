using BCrypt.Net;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
    public class PasswordHelper : IPasswordHelper
    {
        public string GetHashedPassword(string rawPassword, string salt)
        {
           var hashedpassword = BCrypt.Net.BCrypt.HashPassword(rawPassword, salt);

            return hashedpassword;
        }

        public string GetSalt()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            return salt;
        }
    }
}
