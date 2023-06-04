using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IContainer
{
    public interface IMessageContainer
    {
        void CreateAndSaveMessage(string subject, string body, Guid senderId, Guid recipientId);
    }
}
