using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Dto
{
    public class PostPageDto
    {

        public PostPageDto() { }    

        public string Author { get; set; }

        public string CommunityName { get; set; }   

        public DateTime DateCreated { get; set; }

        public  string Title { get; set; }

        public string? Body { get; set; }    

        public string? ImageUrl { get; set; }

        public int Score { get; set; }  

        public Guid PostId { get; set; }

        public List<Guid>UpvotedUserIds { get; set; }

        public List<Guid>DownvotedUserIds { get; set; }

        public bool IsUpvoted { get; set; }

        public bool IsDownvoted { get; set; }

        public bool IsReported { get; set; }    
    }
}
