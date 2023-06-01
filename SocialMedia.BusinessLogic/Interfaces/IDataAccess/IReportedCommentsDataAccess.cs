using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IReportedCommentsDataAccess
	{
		public void CreateRecord(Guid commentId, Guid userId);

		public void DeleteRecord(Guid commentId, Guid userId);

		public bool CheckRecordExists(Guid commentId, Guid userId);
	}
}
