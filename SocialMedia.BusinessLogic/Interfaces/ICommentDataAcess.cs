﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface ICommentDataAcess
    {
        void SaveComment(Comment comment);

        void UpdateComment(Comment comment);

        List<Comment> LoadComment();

        void DeleteComment(Guid id);


    }
}
