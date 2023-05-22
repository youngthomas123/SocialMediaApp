using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Custom_exception
{
    public class InvalidInputException : Exception
    {

        public InvalidInputException() : base("The object has invalid input")
        {
            // This constructor sets the default message to "Username is invalid".
        }

        public InvalidInputException(string message) : base(message)
        {
            // This constructor allows specifying a custom message.
        }

    }
}
