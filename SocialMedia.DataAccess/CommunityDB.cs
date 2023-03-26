using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
    public class CommunityDB : ICommunityDataAcess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteCommunity(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Community> LoadCommunity()
        {
            throw new NotImplementedException();
        }

        public void SaveCommunity()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommunity(Community community)
        {
            throw new NotImplementedException();
        }
    }
}
