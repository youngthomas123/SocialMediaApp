using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class User
	{
		public User() { }
		public User(string userName, string password, string email)
		{
			UserName = userName;
			Password = password;
			Email = email;
			DateCreated = DateTime.Now;
		}

		public User(string userName, string password)
		{
			UserName=userName;
			Password=password;
			Email = null;
			DateCreated = DateTime.Now;
		}

		public string UserName { get; set; }

		public string Password { get;set; }

		public string? Email { get;set; }

		public DateTime DateCreated { get;set; }

		
	}
}
