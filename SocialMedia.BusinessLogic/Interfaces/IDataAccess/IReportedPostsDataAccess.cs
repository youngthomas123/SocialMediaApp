using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IReportedPostsDataAccess
	{
		void CreateRecord(Guid postId, Guid userId, int reasonId);

		void DeleteRecord(Guid postId, Guid userId);

		void DeleteRecord(Guid postId);

		bool CheckRecordExists(Guid postId, Guid userId);
	}
}
