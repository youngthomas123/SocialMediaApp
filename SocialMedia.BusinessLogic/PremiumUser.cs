using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
    public class PremiumUser : User
    {
        public PremiumUser(string userName, string password, string email) : base(userName, password, email)
        {
            UserCreatedCommunities = new List<string>();
        }

        public PremiumUser (Guid userId, string userName, string password, string salt, string? email, DateTime dateCreated) : base(userId, userName, password, salt, email, dateCreated)
        {
            UserCreatedCommunities = new List<string>();
        }

        public List<string> UserCreatedCommunities { get;  private set; }

        public void AddToUserCreatedCommunities(string communityName)
        {
            UserCreatedCommunities.Add(communityName);
        }

    }
}
