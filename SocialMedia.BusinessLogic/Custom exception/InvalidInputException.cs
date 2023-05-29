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
            
        }

        public InvalidInputException(string message) : base(message)
        {
            
        }

    }
}
