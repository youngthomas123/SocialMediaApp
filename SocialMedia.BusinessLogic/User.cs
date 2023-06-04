using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public abstract class User : IUser
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
			UserCreatedPosts = new List<Post>();
			UserCreatedComments = new List<Comment>();
			UserFollowingCommunities = new List<string>();
			UserModeratingCommunities = new List<string>();
            ReceivedMessages = new List<Message>();
			UserFriends = new List<string>();

        }


        public User(Guid userId, string userName, string password, string salt, string? email, DateTime dateCreated) // to load from UserDB
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Salt = salt;
            Email = email;
            DateCreated = dateCreated;
            UserCreatedPosts = new List<Post>();
            UserCreatedComments = new List<Comment>();
            UserFollowingCommunities = new List<string>();
            UserModeratingCommunities = new List<string>();
            ReceivedMessages = new List<Message>();
            UserFriends = new List<string>();
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

		public List<Post>?UserCreatedPosts { get; private set; }

		public List<Comment>?UserCreatedComments { get; private set; }

		public List<string>?UserFollowingCommunities { get; private set; }

		public List<string>?UserModeratingCommunities { get; private set; }

		public List<Message>?ReceivedMessages { get; private set; }

		public List<string>?UserFriends { get; private set; }



		public void AddToUserCreatedPosts(Post post)
		{
			UserCreatedPosts.Add(post);
		}

		public void AddToUserCreatedComments(Comment comment)
		{
			UserCreatedComments.Add(comment);
		}

		public void AddToUserFollowingCommunities(string communityName)
		{
			UserFollowingCommunities.Add(communityName);
		}

		public void AddToUserModeratingCommunities(string communityName)
		{
			UserModeratingCommunities.Add(communityName);
		}

		public void AddToUserFriends(string friendName)
		{
			UserFriends.Add(friendName);
		}

		public void AddToReceivedMessages(Message message)
		{
			ReceivedMessages.Add(message);
		}

        public void SetHashedPassword(string password)
		{
			Password = password;
		}
		public void SetSalt(string salt)
		{
			Salt = salt;

        }

		public void UpdateProfile(string? bio, string? gender, string? location, byte[]? pic)
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
