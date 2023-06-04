using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface ICommunityModeratorsDataAccess
	{
		public void CreateRecord(Guid communityId, Guid moderatorId);

		public void DeleteRecord(Guid communityId, Guid moderatorId);

		public bool CheckRecordExists(Guid communityId, Guid moderatorId);

		List<Guid> LoadCommunityIdsByUser(Guid userId);

    }
}
