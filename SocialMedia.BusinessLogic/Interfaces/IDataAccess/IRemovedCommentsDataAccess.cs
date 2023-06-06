using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IRemovedCommentsDataAccess
	{
		void CreateRecord(Guid commentId);

		void DeleteRecord(Guid commentId);

		bool CheckRecordExists(Guid commentId);

		List<Guid> GetRemovedCommentIds();
	}
}
