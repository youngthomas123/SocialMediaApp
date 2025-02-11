﻿using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IContainer
{
    public interface ICommentContainer
    {
        void Upvote(Guid commentId, string direction, Guid userId);

        void RemoveUpvote(Guid commentId, string direction, Guid userId);

        void Downvote(Guid commentId, string direction, Guid userId);

        void RemoveDownvote(Guid commentId, string direction, Guid userId);
        List<Comment> GetComments();

        void AddComment(Comment comment);

        List<Comment> LoadCommentInPost(Guid PostId);

        List<CommentPageDto> GetCommentPageDtosInPost(Guid postId, Guid userId);

        Comment LoadCommentById(Guid commentId);

        
        bool IsCommentUpvoted(Guid userId, Guid commentId);

        bool IsCommentDownvoted(Guid userId, Guid commentId);

        bool IsCommentReported(Guid userId, Guid commentId);


		bool IsCommentRemoved(Guid commentId);


		void UpdateComment(Guid commentId, string body, Guid LoggedInUserId);

        void DeleteComment(Guid commentId, Guid LoggedInUserId);

        List<ReportReasonsDto> LoadReportReasonsDtos();

        void ReportComment(Guid commentId, Guid userId, int reasonId);

        List<Comment> LoadAllReportedComments();

        int GetNumberOfReportsInComment(Guid commentId);

        void RemoveComment(Guid commentId, Guid moderatorId);



	}
}
