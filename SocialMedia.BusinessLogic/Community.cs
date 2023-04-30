using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Community
	{

		
		public Community(Guid userId, string communityName, string description)
		{
			Guid guid = Guid.NewGuid();

			DateCreated = DateTime.Now;
			CommunityId = guid;
			UserId = userId;
			CommunityName = communityName;
			Description = description;
			Rules = null;	
			FollowingUserIds = null;
			PostIds = null;
		}
		public Community(DateTime dateCreated, string communityName, string description, Guid communityId, Guid userId )
		{
            DateCreated = dateCreated;
			CommunityName = communityName;
			Description = description;
			CommunityId = communityId;
			UserId = userId;
            Rules = new List<string>();
            FollowingUserIds = new List<Guid>();
            PostIds = new List<Guid>();

        }

		public DateTime DateCreated { get; private set; }

		public Guid CommunityId { get; private set; }	

		public Guid  UserId { get; private set; }

		public string CommunityName { get; private set; }	

		public string Description { get; private set; }

		public List<string>? Rules { get;  set; }	// a list of rules

		public List<Guid>? FollowingUserIds { get;  set; } // usernames

		public List <Guid>? PostIds { get;  set; } // post Ids
	} 
}
