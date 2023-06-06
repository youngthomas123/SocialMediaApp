using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IRemovedPostsDataAccess
	{
		void CreateRecord(Guid postId);

		void DeleteRecord(Guid postId);

		bool CheckRecordExists(Guid postId);

		List<Guid> GetRemovedPostIds();
	}
}
