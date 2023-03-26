using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class MessageContainer
    {

        private readonly IMessageDataAcess messageDataAcess;

        public MessageContainer (IMessageDataAcess dataAcess)
        {
            messageDataAcess = dataAcess;
        }
    }
}
