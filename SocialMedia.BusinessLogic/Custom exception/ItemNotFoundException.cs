using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Custom_exception
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base("Was unable to find the item")
        {
            // This constructor sets the default message to "Username is invalid".
        }

        public ItemNotFoundException(string message) : base(message)
        {
            // This constructor allows specifying a custom message.
        }
    }
}
