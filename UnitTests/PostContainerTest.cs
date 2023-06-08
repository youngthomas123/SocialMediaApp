using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SocialMedia.BusinessLogic;
using System.Diagnostics;
using SocialMedia.BusinessLogic.Custom_exception;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace UnitTests
{
    [TestClass]
    public class PostContainerTest
    {
        
        private Mock<IPostDataAccess> postDataAccessMock;
        private Mock<IUserDataAccess> userDataAccessMock;
        private Mock<ICommunityDataAccess> communityDataAccessMock;
        private Mock<IUpvotedPostsDataAccess> upvotedPostsDataAccessMock;
        private Mock<IDownvotedPostsDataAccess> downvotedPostsDataAccessMock;
        private Mock<ICommentDataAccess> commentDataAccessMock;
        private Mock<IUpvotedCommentsDataAccess> upvotedCommentsDataAccessMock;
        private Mock<IDownvotedCommentsDataAccess> downvotedCommentsDataAccessMock;
        private Mock<IReportedPostsDataAccess> reportedPostsDataAccessMock;
        private Mock<IReportedCommentsDataAccess> reportedCommentsDataAccessMock;
        private Mock<IReportReasonsDataAccess> reportReasonsDataAccessMock;
        private Mock<IRemovedPostsDataAccess> removedPostsDataAccessMock;
        private Mock<ICommunityModeratorsDataAccess> communityModeratorsDataAccessMock;
        private Mock<IRemovedCommentsDataAccess> removedCommentsDataAccessMock;
        private Mock<IContentFilterAndRanking> contentFilterAndRankingMock;
        private PostContainer postContainer;

        [TestInitialize]
        public void Setup()
        {
            postDataAccessMock = new Mock<IPostDataAccess>();
            userDataAccessMock = new Mock<IUserDataAccess>();
            communityDataAccessMock = new Mock<ICommunityDataAccess>();
            upvotedPostsDataAccessMock = new Mock<IUpvotedPostsDataAccess>();
            downvotedPostsDataAccessMock = new Mock<IDownvotedPostsDataAccess>();
            commentDataAccessMock = new Mock<ICommentDataAccess>();
            upvotedCommentsDataAccessMock = new Mock<IUpvotedCommentsDataAccess>();
            downvotedCommentsDataAccessMock = new Mock<IDownvotedCommentsDataAccess>();
            reportedPostsDataAccessMock = new Mock<IReportedPostsDataAccess>();
            reportedCommentsDataAccessMock = new Mock<IReportedCommentsDataAccess>();
            reportReasonsDataAccessMock = new Mock<IReportReasonsDataAccess>();
            removedPostsDataAccessMock = new Mock<IRemovedPostsDataAccess>();
            communityModeratorsDataAccessMock = new Mock<ICommunityModeratorsDataAccess>();
            removedCommentsDataAccessMock = new Mock<IRemovedCommentsDataAccess>();
            contentFilterAndRankingMock = new Mock<IContentFilterAndRanking>();

            postContainer = new PostContainer(
                postDataAccessMock.Object,
                userDataAccessMock.Object,
                communityDataAccessMock.Object,
                upvotedPostsDataAccessMock.Object,
                downvotedPostsDataAccessMock.Object,
                commentDataAccessMock.Object,
                upvotedCommentsDataAccessMock.Object,
                downvotedCommentsDataAccessMock.Object,
                reportedPostsDataAccessMock.Object,
                reportReasonsDataAccessMock.Object,
                reportedCommentsDataAccessMock.Object,
                removedPostsDataAccessMock.Object,
                communityModeratorsDataAccessMock.Object,
                removedCommentsDataAccessMock.Object,
                contentFilterAndRankingMock.Object
            );
        }

        [TestMethod]
        [DataRow("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2", "Test Title 1", "Test Body 1", null, "1b02981f-4687-4a63-9d4c-1af2eae82759")]
        [DataRow("2f74fb21-1e36-4b53-8f95-84a71aaae377", "Test Title 2", null, "Test ImageUrl 2", "3c8f02c1-9d68-4c0a-8a5c-0be2e1e6d791")]
        [DataRow("d831b8ef-4d84-4dc1-85a2-b9f8a216a67a", "Test Title 3", "Test Body 3", null, "f2876496-8e50-41d0-b04a-2f35c0a9ae6c")]

        public void CreateAndSavePost_ValidInput_CallsSavePost(string userId,string title,string body, string imageUrl,string communityId)
        {
            // Arrange
            Guid userIdGuid = new Guid(userId);
            Guid communityIdGuid = Guid.Parse(communityId);


            // Act
            postContainer.CreateAndSavePost(userIdGuid, title, body, imageUrl, communityIdGuid);

            // Assert
            postDataAccessMock.Verify(repository => repository.SavePost(It.IsAny<Post>()), Times.Once);
        }

        [TestMethod]
        [DataRow("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2", "Test Title 1", "Test Body 1", "ImageUrl", "1b02981f-4687-4a63-9d4c-1af2eae82759")]
        [DataRow("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2", "Test Title 1", null, null, "1b02981f-4687-4a63-9d4c-1af2eae82759")]

        public void CreateAndSavePost_InvalidInput_SavePostNotClalled(string userId, string title, string body, string imageUrl, string communityId)
        {
            // Arrange
            Guid userIdGuid = new Guid(userId);
            Guid communityIdGuid = Guid.Parse(communityId);

            // Act
            postContainer.CreateAndSavePost(userIdGuid, title, body, imageUrl, communityIdGuid);

            // Assert
            postDataAccessMock.Verify(repository => repository.SavePost(It.IsAny<Post>()), Times.Never);


        }

        [TestMethod]
        public void CreateAndSavePost_InvalidInput_ThowsInvalidInputException()
        {
            // Arrange
            Guid userId = new Guid();
            string title = string.Empty;
            string body = "Body";
            string imageUrl = null;

            Guid communityId = new Guid();



            // Act & Assert
            Assert.ThrowsException<InvalidInputException>(() =>
            {
                postContainer.CreateAndSavePost(userId, title, body, imageUrl, communityId);
            });

        }

        [TestMethod]
        public void Upvote_WhenPostIdExistsAndUserHasNotUpvoted_IncreasesUpvotesBy1()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title,body, upvotes, downvotes,communityId, imageUrl);

            

          

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            upvotedPostsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, postId)).Returns(false);
            upvotedPostsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            downvotedPostsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            postDataAccessMock.Setup(d => d.LoadPostById(postId)).Returns(post);

            // Act
            postContainer.Upvote(postId, direction, userId);

            // Assert
            Assert.AreEqual(upvotes + 1, post.Upvotes);
        }

        [TestMethod]
        public void Upvote_WhenPostIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

            

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(false);


            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                postContainer.Upvote(postId, direction, userId);
            });


        }

        [TestMethod]
        public void Upvote_WhenPostIdExistsAndUserHasAlreadyUpvoted_ThrowsAccessException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

          

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            upvotedPostsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, postId)).Returns(true);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                postContainer.Upvote(postId, direction, userId);
            });

        }


        [TestMethod]
        public void RemoveUpvote_ValidInput_DecreasesUpvotesBy1()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "upvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 1;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

            



            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            upvotedPostsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, postId)).Returns(true);
            upvotedPostsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            downvotedPostsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            postDataAccessMock.Setup(d => d.LoadPostById(postId)).Returns(post);

            // Act
            postContainer.RemoveUpvote(postId, direction, userId);

            // Assert
            Assert.AreEqual(upvotes-1, post.Upvotes);
        }

        [TestMethod]
        public void RemoveUpvote_WhenPostIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

          

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(false);


            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                postContainer.RemoveUpvote(postId, direction, userId);
            });
        }

        [TestMethod]
        public void RemoveUpvote_WhenPostIdExistsButUserHasNotUpvotedPost_ThrowsAccessException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeUpvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

           

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            upvotedPostsDataAccessMock.Setup(d => d.HasUserUpvoted(userId, postId)).Returns(false);


            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                postContainer.RemoveUpvote(postId, direction, userId);
            });
        }

        [TestMethod]
        public void Downvote_ValidInput_IncreasesDownvotesBy1()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "downvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);



            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            downvotedPostsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, postId)).Returns(false);
            upvotedPostsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            downvotedPostsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            postDataAccessMock.Setup(d => d.LoadPostById(postId)).Returns(post);



            // Act
            postContainer.Downvote(postId, direction, userId);

            // Assert
            Assert.AreEqual(downvotes + 1, post.Downvotes);
        }

        [TestMethod]
        public void Downvote_WhenPostIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "downvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);



            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(false);


            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                postContainer.Downvote(postId, direction, userId);
            });
        }

        [TestMethod]
        public void Downvote_WhenPostIdExistsButUserHasAlreadyDownvotedPost_ThrowsAccessException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "downvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);



            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            downvotedPostsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, postId)).Returns(true);


            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                postContainer.Downvote(postId, direction, userId);
            });
        }

        [TestMethod]
        public void RemoveDownvote_ValidInput_DecreasesDownvotesBy1()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeDownvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 1;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);


            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            downvotedPostsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, postId)).Returns(true);
            upvotedPostsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            downvotedPostsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            postDataAccessMock.Setup(d => d.LoadPostById(postId)).Returns(post);

            // Act
            postContainer.RemoveDownvote(postId, direction, userId);

            // Assert
            Assert.AreEqual(downvotes - 1, post.Downvotes);
        }

        [TestMethod]
        public void RemoveDownvote_WhenPostIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeDownvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);



            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(false);


            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                postContainer.RemoveDownvote(postId, direction, userId);
            });
        }

        [TestMethod]
        public void RemoveDownvote_WhenPostIdExistsButUserHasNotDownvotedPost_ThrowsAccessException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
            string direction = "removeDownvotePost";
            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);



            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            downvotedPostsDataAccessMock.Setup(d => d.HasUserDownvoted(userId, postId)).Returns(false);

            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                postContainer.RemoveDownvote(postId, direction, userId);
            });

        }

        [TestMethod]
        public void LoadAllPosts_ReturnsPosts()
        {
            Random random = new Random();

            // Arrange

            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");
           
            Guid userId = Guid.NewGuid();
            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = random.Next(-10, 101);
            int downvotes = random.Next(-10, 101);
            Guid communityId = new Guid();


            List<Post> expectedPosts = new List<Post>()
            {
               new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl), 
               new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl)
            };

            postDataAccessMock.Setup(d => d.LoadPost()).Returns(expectedPosts);
            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            upvotedPostsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            downvotedPostsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByPost(postId)).Returns(new List<Guid>());

            // Act
            List<Post> actualPosts = postContainer.LoadAllPosts();

            // Assert
            postDataAccessMock.Verify(d => d.LoadPost(), Times.Once);
            // Assert that the returned list of posts matches the expected list
            CollectionAssert.AreEqual(expectedPosts, actualPosts);
        }

        [TestMethod]
        public void LoadPostById_ValidInput_ReturnsPost()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post expectedPost = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            postDataAccessMock.Setup(d => d.LoadPostById(postId)).Returns(expectedPost);
            upvotedPostsDataAccessMock.Setup(d => d.GetUpvotedUserIdsByPost(postId)).Returns(new List<Guid>());
            downvotedPostsDataAccessMock.Setup(d => d.GetDownvotedUserIdsByPost(postId)).Returns(new List<Guid>());

            // Act
            Post actualPost = postContainer.LoadPostById(postId);

            // Assert
            // Verify that DoesPostIdExist was called with the expected postId
            postDataAccessMock.Verify(d => d.DoesPostIdExist(postId), Times.Once);

            // Verify that LoadPostById was called with the expected postId
            postDataAccessMock.Verify(d => d.LoadPostById(postId), Times.Once);

            // Assert that the returned post matches the expected post
            Assert.AreEqual(expectedPost, actualPost);
        }

        [TestMethod]
        public void UpdatePost_ValidPostInput_CallsUpdatePostMethod()
        {
            // Arrange

            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

            postDataAccessMock.Setup(d => d.UpdatePost(post));

            // Act
            postContainer.UpdatePost(post);

            // Assert
            // Verify that UpdatePost was called with the expected post
            postDataAccessMock.Verify(d => d.UpdatePost(post), Times.Once);
        }

        [TestMethod]
        [DataRow("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2", "Test Title 1", "Test Body 1", "ImageUrl", "1b02981f-4687-4a63-9d4c-1af2eae82759")]
        [DataRow("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2", "Test Title 1", null, null, "1b02981f-4687-4a63-9d4c-1af2eae82759")]
        public void UpdatePost_InvalidPostInput_ThrowsInvalidInputException(string userId, string title, string body, string imageUrl, string communityId)
        {
            // Arrange
            Guid userIdGuid = new Guid(userId);
            Guid communityIdGuid = Guid.Parse(communityId);
            Post post = new Post(userIdGuid, title, body, imageUrl, communityIdGuid);

            // Act & Assert
            Assert.ThrowsException<InvalidInputException>(() =>
            {
                postContainer.UpdatePost(post);
            });

        }

        [TestMethod]
        public void UpdatePost_WhenPostIdExistsAndUserIsOwner_CallsUpdatePost()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            postDataAccessMock.Setup(d => d.LoadPostById(postId)).Returns(post);
            postDataAccessMock.Setup(d => d.UpdatePost(postId, title, body, imageUrl));

            // Act
            postContainer.UpdatePost(postId, title, body, imageUrl, userId);

            // Assert
            // Verify that DoesPostIdExist was called with the expected postId
            postDataAccessMock.Verify(d => d.DoesPostIdExist(postId), Times.Once);

            // Verify that LoadPostById was called with the expected postId
            postDataAccessMock.Verify(d => d.LoadPostById(postId), Times.Once);

            // Verify that UpdatePost was called with the expected arguments
            postDataAccessMock.Verify(d => d.UpdatePost(postId, title, body, imageUrl), Times.Once);
        }

        [TestMethod]
        public void UpdatePost_WhenPostIdExistsButUserIsNotTheOwner_ThrowAccessException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid LoggedInUserId = new Guid();

            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(true);
            postDataAccessMock.Setup(d => d.LoadPostById(postId)).Returns(post);
            postDataAccessMock.Setup(d => d.UpdatePost(postId, title, body, imageUrl));


            // Act & Assert
            Assert.ThrowsException<AccessException>(() =>
            {
                postContainer.UpdatePost(postId, title, body, imageUrl, LoggedInUserId);
            });

        }

        [TestMethod]
        public void UpdatePost_WhenPostIdDoesNotExist_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid postId = new Guid("0ec5e11f-7927-4ebf-a224-fd6c7589b2d2");

            Guid userId = Guid.NewGuid();

            DateTime datecreated = DateTime.Now;
            string title = "Title";
            string body = "body";
            string imageUrl = null;
            int upvotes = 0;
            int downvotes = 0;
            Guid communityId = new Guid();

            Post post = new Post(datecreated, postId, userId, title, body, upvotes, downvotes, communityId, imageUrl);

            postDataAccessMock.Setup(d => d.DoesPostIdExist(postId)).Returns(false);


            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                postContainer.UpdatePost(postId, title, body, imageUrl, userId);
            });
        }
    }

}

