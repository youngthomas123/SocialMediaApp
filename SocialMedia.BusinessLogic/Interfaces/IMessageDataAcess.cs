using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface IMessageDataAcess
    {
        void SaveMessage(Message message);

        void UpdateMessage(Message message);

        List<Message> LoadMessage();

        void DeleteMessage(Guid id);



    }
}
