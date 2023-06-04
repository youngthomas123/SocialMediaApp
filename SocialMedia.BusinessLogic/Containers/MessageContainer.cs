using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace SocialMedia.BusinessLogic.Containers
{
    public class MessageContainer : IMessageContainer
    {

        private readonly IMessageDataAccess _messageDataAccess;
        private readonly IUserDataAccess _userDataAccess;

        public MessageContainer (IMessageDataAccess messageDataAccess, IUserDataAccess userDataAccess)
        {
            _messageDataAccess = messageDataAccess;
            _userDataAccess = userDataAccess;
        }

        public void CreateAndSaveMessage(string subject, string body, Guid senderId, Guid recipientId)
        {
            var doesSenderIdExist = _userDataAccess.DoesUserIdExist(senderId);
            var doesRecipientIdExist = _userDataAccess.DoesUserIdExist(recipientId);

            if(doesSenderIdExist == true && doesRecipientIdExist == true)
            {
                if (subject != null && body != null && subject.Length <= 50 && body.Length <= 150)
                {
                    Message message = new Message(subject, body, senderId, recipientId);
                    _messageDataAccess.SaveMessage(message);
                }
                else
                {
                    throw new InvalidInputException("Message body or subject to long");
                }
            }
            else
            {
                throw new ItemNotFoundException("Invaild Sender or Recipient Id");
            }
            
        }
    }
}
