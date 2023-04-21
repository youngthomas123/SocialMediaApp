using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class User
	{
		
		public User(string userName, string password, string email)
		{
            Guid guid = Guid.NewGuid();
			UserId = guid;
            UserName = userName;
			Password = password;
			Email = email;
			DateCreated = DateTime.Now;
		}

		

		

        public User(Guid userId, string userName, string password, string salt, string? email, DateTime dateCreated) // to load from DB
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Salt = salt;
            Email = email;
            DateCreated = dateCreated;
        }

        public Guid UserId { get; private set; }
		public string UserName { get; private  set; }

		public string Password { get; private set; }

		public string Salt { get; private set; }

		public string? Email { get; private set; }

		public DateTime DateCreated { get; private set; }

		public void SetHashedPassword(string password)
		{
			Password = password;
		}
		public void SetSalt(string salt)
		{
			Salt = salt;

        }
		
	}
}
