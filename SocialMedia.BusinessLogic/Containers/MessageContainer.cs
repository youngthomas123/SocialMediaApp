using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class MessageContainer : IMessageContainer
    {

        private readonly IMessageDataAccess _messageDataAccess;

        public MessageContainer (IMessageDataAccess messageDataAccess)
        {
            _messageDataAccess = messageDataAccess;
        }

        public void CreateAndSaveMessage()
        {

        }
    }
}
