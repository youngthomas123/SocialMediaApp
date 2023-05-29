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
            
        }

        public ItemNotFoundException(string message) : base(message)
        {
          
        }
    }
}
