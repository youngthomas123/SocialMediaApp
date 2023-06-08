using Moq;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic;
using System.ComponentModel.Design;
using SocialMedia.BusinessLogic.Custom_exception;
using System.Xml.Linq;
using SocialMedia.BusinessLogic.Dto;
using Castle.Components.DictionaryAdapter;

namespace UnitTests
{
    [TestClass]
    public class CommentContainerTest
    {
        private CommentContainer commentContainer;
        private Mock<ICommentDataAccess> commentDataAccessMock;
        private Mock<IUserDataAccess> userDataAccessMock;
        private Mock<IUpvotedCommentsDataAccess> upvotedCommentsDataAccessMock;
        private Mock<IDownvotedCommentsDataAccess> downvotedCommentsDataAccessMock;
        private Mock<IPostDataAccess> postDataAccessMock;
        private Mock<IReportedCommentsDataAccess> reportedCommentsDataAccessMock;
        private Mock<IReportReasonsDataAccess> reportReasonsDataAccessMock;
        private Mock<IRemovedCommentsDataAccess> removedCommentsDataAccessMock;
        private Mock<IContentFilterAndRanking> contentFilterAndRankingMock;

        [TestInitialize]
        public void Setup()
        {
            commentDataAccessMock = new Mock<ICommentDataAccess>();
            userDataAccessMock = new Mock<IUserDataAccess>();
            upvotedCommentsDataAccessMock = new Mock<IUpvotedCommentsDataAccess>();
            downvotedCommentsDataAccessMock = new Mock<IDownvotedCommentsDataAccess>();
            postDataAccessMock = new Mock<IPostDataAccess>();
            reportedCommentsDataAccessMock = new Mock<IReportedCommentsDataAccess>();
            reportReasonsDataAccessMock = new Mock<IReportReasonsDataAccess>();
            removedCommentsDataAccessMock = new Mock<IRemovedCommentsDataAccess>();
            contentFilterAndRankingMock = new Mock<IContentFilterAndRanking>();

            commentContainer = new CommentContainer(
                commentDataAccessMock.Object,
                userDataAccessMock.Object,
                upvotedCommentsDataAccessMock.Object,
                downvotedCommentsDataAccessMock.Object,
                postDataAccessMock.Object,
                reportedCommentsDataAccessMock.Object,
                reportReasonsDataAccessMock.Object,
                removedCommentsDataAccessMock.Object,
                contentFilterAndRankingMock.Object
            );
        }

        [TestMethod]
        public void Upvote_WhenCommentIdExistsAndUserHasNotUpvoted_IncreasesUpvotesBy1()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act
            commentContainer.Upvote(commentId, direction, userId);

            // Assert
            Assert.AreEqual(upvotes + 1, comment.Upvotes);
        }

        [TestMethod]
        public void Upvote_CommentIdExistsButUserHasAlreadyUpvoted_ThrowsAccessException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                commentContainer.Upvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void Upvote_WhenCommentIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                commentContainer.Upvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void RemoveUpvote_ValidInput_DecreasesUpvotesBy1()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act
            commentContainer.RemoveUpvote(commentId, direction, userId);

            // Assert
            Assert.AreEqual(upvotes - 1, comment.Upvotes);
        }

        [TestMethod]
        public void RemoveUpvote_WhenCommentIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);


            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                commentContainer.RemoveUpvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void RemoveUpvote_WhenCommentIdExistsButUserHasNotUpvotedComment_ThrowsAccessException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                commentContainer.RemoveUpvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void Downvote_ValidInput_IncreasesDownvotesBy1()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            downvotedCommentsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act
            commentContainer.Downvote(commentId, direction, userId);

            // Assert
            Assert.AreEqual(downvotes + 1, comment.Downvotes);
        }

        [TestMethod]
        public void Downvote_WhenCommentIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(false);
            downvotedCommentsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);


            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                commentContainer.Downvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void Downvote_WhenCommentIdExistsButUserHasAlreadyDownvoted_ThrowAccessException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            downvotedCommentsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                commentContainer.Downvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void RemoveDownvote_ValidInput_DecreasesDownvotesBy1()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            downvotedCommentsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act
            commentContainer.RemoveDownvote(commentId, direction, userId);

            // Assert
            Assert.AreEqual(downvotes - 1, comment.Downvotes);
        }

        [TestMethod]
        public void RemoveDownvote_WhenCommentIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(false);
            downvotedCommentsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, commentId)).Returns(true);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                commentContainer.RemoveDownvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void RemoveDownvote_WhenCommentIdExistsButUserHasNotDownvoted_ThrowsAccessException()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvoteComment";
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);


            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            downvotedCommentsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, commentId)).Returns(false);
            upvotedCommentsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            downvotedCommentsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByComment(commentId)).Returns(new List<Guid>());
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                commentContainer.RemoveDownvote(commentId, direction, userId);
            });
        }

        [TestMethod]
        public void AddComment_ValidComment_SaveCommentCalled()
        {
            // Arrange
            Guid commentId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            Guid postId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);

            // Act
            commentContainer.AddComment(comment);

            // Assert
            commentDataAccessMock.Verify(d => d.SaveComment(comment), Times.Once);
        }


        [TestMethod]
        [DataRow("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2", null, "1b02981f-4687-4a63-9d4c-1af2eae82759")]
        [DataRow("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2", "", "1b02981f-4687-4a63-9d4c-1af2eae82759")]
        public void AddComment_InvalidComment_ThowsInvalidInputException(string userId, string body, string postId)
        {
            Guid UserId = new Guid(userId);
            Guid PostId = new Guid(postId);

            Comment comment = new Comment(UserId, body, PostId);

            // Act & Assert
            Assert.ThrowsException<InvalidInputException>(() =>
            {
                commentContainer.AddComment(comment);
            });

        }
        [TestMethod]
        public void LoadCommentInPost_ExistingPostId_ReturnsComments()
        {
            // Arrange
            var postId = Guid.NewGuid();

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);

            var comments = new List<Comment>
            {
              new Comment(new Guid(), "body1", postId),
              new Comment(new Guid(), "bidy2", postId),
              new Comment(new Guid(), "body3", postId)
            };


            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            commentDataAccessMock.Setup(d => d.LoadCommentsInPost(postId)).Returns(comments);



            // Act
            var result = commentContainer.LoadCommentInPost(postId);

            // Assert
            CollectionAssert.AreEqual(comments, result);
        }

        [TestMethod]
        public void LoadCommentInPost_NonExistingPostId_ThrowsItemNotFoundException()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var comments = new List<Comment>
            {
              new Comment(new Guid(), "body1", postId),
              new Comment(new Guid(), "bidy2", postId),
              new Comment(new Guid(), "body3", postId)
            };

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(false);
            commentDataAccessMock.Setup(d => d.LoadCommentsInPost(postId)).Returns(comments);


            // Act and Assert
            Assert.ThrowsException<ItemNotFoundException>(() => commentContainer.LoadCommentInPost(postId));
        }

        [TestMethod]
        public void UpdateComment_WhenCommentIdExistsAndUserIsOwner_CallsUpdateComment()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid LoggedInUserId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = LoggedInUserId;
            Guid commentId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);

            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            userDataAccessMock.Setup(d => d.DoesUserIdExist(LoggedInUserId)).Returns(true);
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);
            commentDataAccessMock.Setup(d => d.UpdateComment(commentId, body));

            // Act
            commentContainer.UpdateComment(commentId, body, LoggedInUserId);

            // Assert

            commentDataAccessMock.Verify(d => d.DoesCommentIdExist(commentId), Times.Once);


            commentDataAccessMock.Verify(d => d.LoadCommentById(commentId), Times.Once);


            commentDataAccessMock.Verify(d => d.UpdateComment(commentId, body), Times.Once);
        }

        [TestMethod]
        public void UpdateComment_WhenCommentIdExistsButUserIsNotTheOwner_ThrowsAccessException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid LoggedInUserId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = new Guid();
            Guid commentId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);

            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(true);
            userDataAccessMock.Setup(d => d.DoesUserIdExist(LoggedInUserId)).Returns(true);
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);
            commentDataAccessMock.Setup(d => d.UpdateComment(commentId, body));

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                commentContainer.UpdateComment(commentId, body, LoggedInUserId);
            });

        }

        [TestMethod]
        public void UpdateComment_WhenCommentIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid LoggedInUserId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = LoggedInUserId;
            Guid commentId = new Guid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);

            commentDataAccessMock.Setup(d => d.DoesCommentIdExist(commentId)).Returns(false);
            userDataAccessMock.Setup(d => d.DoesUserIdExist(LoggedInUserId)).Returns(true);
            commentDataAccessMock.Setup(d => d.LoadCommentById(commentId)).Returns(comment);
            commentDataAccessMock.Setup(d => d.UpdateComment(commentId, body));

            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                commentContainer.UpdateComment(commentId, body, LoggedInUserId);
            });
        }

        [TestMethod]
        public void DeleteComment_ValidInputAndUserIsOwner_DeletesTheComment()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid LoggedInUserId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = LoggedInUserId;
            Guid commentId = Guid.NewGuid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);

             commentDataAccessMock.Setup(x => x.DoesCommentIdExist(commentId)).Returns(true);
             userDataAccessMock.Setup(x => x.DoesUserIdExist(LoggedInUserId)).Returns(true);
             commentDataAccessMock.Setup(x => x.LoadCommentById(commentId)).Returns(comment);

            // Act
             commentContainer.DeleteComment(commentId, userId);

            // Assert
             removedCommentsDataAccessMock.Verify(x => x.DeleteRecord(commentId), Times.Once);
             reportedCommentsDataAccessMock.Verify(x => x.DeleteRecord(commentId), Times.Once);
             upvotedCommentsDataAccessMock.Verify(x => x.DeleteRecord(commentId), Times.Once);
             downvotedCommentsDataAccessMock.Verify(x => x.DeleteRecord(commentId), Times.Once);
             commentDataAccessMock.Verify(x => x.DeleteComment(commentId), Times.Once);


        }

        [TestMethod]
        public void DeleteComment_ValidInputButUserIsNotTheOwner_ThrowsAccessException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid LoggedInUserId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = Guid.NewGuid();
            Guid commentId = Guid.NewGuid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);

            commentDataAccessMock.Setup(x => x.DoesCommentIdExist(commentId)).Returns(true);
            userDataAccessMock.Setup(x => x.DoesUserIdExist(LoggedInUserId)).Returns(true);
            commentDataAccessMock.Setup(x => x.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                commentContainer.DeleteComment(commentId, LoggedInUserId);
            });
        }

        [TestMethod]
        public void DeleteComment_WhenCommentIdDoesNotExist_Throws_ItemNotFoundException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid LoggedInUserId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string body = "body";
            int upvotes = 0;
            int downvotes = 0;
            Guid userId = Guid.NewGuid();
            Guid commentId = Guid.NewGuid();

            Comment comment = new Comment(datecreated, commentId, userId, body, postId, upvotes, downvotes);

            commentDataAccessMock.Setup(x => x.DoesCommentIdExist(commentId)).Returns(false);
            userDataAccessMock.Setup(x => x.DoesUserIdExist(LoggedInUserId)).Returns(true);
            commentDataAccessMock.Setup(x => x.LoadCommentById(commentId)).Returns(comment);

            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                commentContainer.DeleteComment(commentId, LoggedInUserId);
            });
        }

        [TestMethod]
        public void ReportComment_ValidInput_CreateReportRecord()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var reasonId = 1;

             commentDataAccessMock.Setup(x => x.DoesCommentIdExist(commentId)).Returns(true);
             userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
            reportedCommentsDataAccessMock.Setup(d => d.CreateRecord(commentId, userId, reasonId));
            reportedCommentsDataAccessMock.Setup(d => d.CheckRecordExists(commentId, userId)).Returns(false);

            // Act
            commentContainer.ReportComment(commentId, userId, reasonId);

            // Assert
             reportedCommentsDataAccessMock.Verify(x => x.CreateRecord(commentId, userId, reasonId), Times.Once);

        }

        [TestMethod]
        public void ReportComment_WhenCommentIdExistsButUserHasAlreadyReported_ThrowsAccessException()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var reasonId = 1;

            commentDataAccessMock.Setup(x => x.DoesCommentIdExist(commentId)).Returns(true);
            userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
            reportedCommentsDataAccessMock.Setup(d => d.CreateRecord(commentId, userId, reasonId));
            reportedCommentsDataAccessMock.Setup(d => d.CheckRecordExists(commentId, userId)).Returns(true);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                commentContainer.ReportComment(commentId, userId, reasonId);
            });
        }

        [TestMethod]
        public void ReportComment_WhenCommentIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var reasonId = 1;

            commentDataAccessMock.Setup(x => x.DoesCommentIdExist(commentId)).Returns(false);
            userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
            reportedCommentsDataAccessMock.Setup(d => d.CreateRecord(commentId, userId, reasonId));
            reportedCommentsDataAccessMock.Setup(d => d.CheckRecordExists(commentId, userId)).Returns(false);

            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                commentContainer.ReportComment(commentId, userId, reasonId);
            });
        }

    }

}

