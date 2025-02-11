﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Custom_exception
{
    public class UserCreationException : Exception
    {
       
            public UserCreationException() : base("Username is invalid")
            {
                // This constructor sets the default message to "Username is invalid".
            }

            public UserCreationException(string message) : base(message)
            {
                // This constructor allows specifying a custom message.
            }
        

    }
}
