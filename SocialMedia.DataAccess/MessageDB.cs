using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
    public class MessageDB : IMessageDataAcess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteMessage(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Message> LoadMessage()
        {
            throw new NotImplementedException();
        }

        public void SaveMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
