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
			Bio = null;
			Gender = null;
			Location = null;
			ProfilePicture = null;

		}

		

		

        public User(Guid userId, string userName, string password, string salt, string? email, DateTime dateCreated) // to load from UserDB
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

		public string? Bio { get;private set; }

		public string? Gender { get; private set; }

		public string? Location { get; private set; }

        public byte[]? ProfilePicture { get; private set; }

        public void SetHashedPassword(string password)
		{
			Password = password;
		}
		public void SetSalt(string salt)
		{
			Salt = salt;

        }

		public void UpdateProfile(string bio, string gender, string location, byte[] pic)
		{
			Bio = bio;
			Gender = gender;
			Location = location;
			ProfilePicture = pic;
		}

		public void UpdateProfilePic(byte[] pic)
		{
			ProfilePicture = pic;
		}
		
	}
}
