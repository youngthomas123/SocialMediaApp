using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
    public class RegularUser : User
    {
        public RegularUser(string userName, string password, string email) : base( userName,  password,  email)
        {

        }
        public RegularUser(Guid userId, string userName, string password, string salt, string? email, DateTime dateCreated) : base( userId,  userName,  password,  salt,   email,  dateCreated)
        {

        }
    }
}
