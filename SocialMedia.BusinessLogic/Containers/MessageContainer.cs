﻿using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class MessageContainer
    {

        private readonly IMessageDataAccess messageDataAcess;

        public MessageContainer (IMessageDataAccess dataAcess)
        {
            messageDataAcess = dataAcess;
        }
    }
}
