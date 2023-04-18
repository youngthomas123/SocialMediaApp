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

		
		public Community(string creator, string name, string description, List<string> rules)
		{
			Guid guid = Guid.NewGuid();

			DateCreated = DateTime.Now;
			CommunityId = guid;
			Creator = creator;
			Name = name;
			Description = description;
			Rules = rules;
			Members = null;
			Posts = null;
		}
		public Community(DateTime dateCreated, string name, string description, Guid communityId, string creator )
		{
            DateCreated = dateCreated;
			Name = name;
			Description = description;
			CommunityId = communityId;
			Creator = creator;
            Rules = new List<string>();
            Members = new List<string>();
            Posts = new List<string>();

        }

		public DateTime DateCreated { get; private set; }

		public Guid CommunityId { get; private set; }	

		public string Creator { get; private set; }

		public string Name { get; private set; }	

		public string Description { get; private set; }

		public List<string>? Rules { get;  set; }	// a list of rules

		public List<string>? Members { get;  set; } // usernames

		public List <string>? Posts { get;  set; } // post Ids
	} 
}
