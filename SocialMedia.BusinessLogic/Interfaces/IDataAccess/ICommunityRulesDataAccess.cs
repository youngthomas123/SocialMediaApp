using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface ICommunityRulesDataAccess
    {
        List<string> LoadRules(Guid communityId);
        void CreateRule(Guid communityId, string rule);

        void DeleteRule(Guid communityId, string rule);

        void DeleteRecords(Guid communityId);




    }
}
