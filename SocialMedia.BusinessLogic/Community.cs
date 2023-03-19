using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Community
	{
		public Community(DateTime dateCreated, string communityId, string creator, string name, string description, List<string> rules)
		{
			DateCreated = dateCreated;
			CommunityId = communityId;
			Creator = creator;
			Name = name;
			Description = description;
			Rules = rules;
			Members = null;
		}

		public DateTime DateCreated { get; set; }

		public string CommunityId { get; set; }	

		public string Creator { get; set; }

		public string Name { get; set; }	

		public string Description { get; set; }

		public List<string> Rules { get; set; }	

		public List<string>? Members { get; set; }
	} 
}
