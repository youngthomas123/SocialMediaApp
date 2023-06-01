using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IReportedPostsDataAccess
	{
		public void CreateRecord(Guid postId, Guid userId);

		public void DeleteRecord(Guid postId, Guid userId);

		public bool CheckRecordExists(Guid postId, Guid userId);
	}
}
