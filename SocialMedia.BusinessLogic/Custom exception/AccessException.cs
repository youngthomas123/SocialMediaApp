using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Custom_exception
{
    public class AccessException : Exception
    {
        public AccessException() : base("You do not have access to modify this item.")
        {
           
        }

        public AccessException(string message) : base(message)
        {
            
        }
    }
}
