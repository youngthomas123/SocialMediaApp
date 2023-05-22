using System;
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
            // This constructor sets the default message to "Username is invalid".
        }

        public ItemNullException(string message) : base(message)
        {
            // This constructor allows specifying a custom message.
        }
    }
}
