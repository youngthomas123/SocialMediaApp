﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Custom_exception
{
    public class ItemNullException : Exception
    {
        public ItemNullException() : base("The Item is null")
        {
           
        }

        public ItemNullException(string message) : base(message)
        {

        }
    }
}
