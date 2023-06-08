using Moq;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;

namespace UnitTests
{
    [TestClass]
    public class UserContainerTest
    {
        private Mock<IUserDataAccess> _userDataAccessMock;
        private Mock<IPasswordHelper> _passwordHelperMock;
        private Mock<IAuthenticationSystem> _authenticationSystemMock;
        private Mock<IProfileDataAccess> _profileDataAccessMock;
        private Mock<IUserFriendsDataAccess> _userFriendsDataAccessMock;
        private Mock<IPostDataAccess> _postDataAccessMock;
        private Mock<ICommentDataAccess> _commentDataAccessMock;
        private Mock<IMessageDataAccess> _messageDataAccessMock;
        private Mock<ICommunityMembersDataAccess> _communityMembersDataAccessMock;
        private Mock<ICommunityModeratorsDataAccess> _communityModeratorsDataAccessMock;
        private Mock<ICommunityDataAccess> _communityDataAccessMock;
        private UserContainer _userContainer;

        [TestInitialize]
        public void Initialize()
        {
            _userDataAccessMock = new Mock<IUserDataAccess>();
            _passwordHelperMock = new Mock<IPasswordHelper>();
            _authenticationSystemMock = new Mock<IAuthenticationSystem>();
            _profileDataAccessMock = new Mock<IProfileDataAccess>();
            _userFriendsDataAccessMock = new Mock<IUserFriendsDataAccess>();
            _postDataAccessMock = new Mock<IPostDataAccess>();
            _commentDataAccessMock = new Mock<ICommentDataAccess>();
            _messageDataAccessMock = new Mock<IMessageDataAccess>();
            _communityMembersDataAccessMock = new Mock<ICommunityMembersDataAccess>();
            _communityModeratorsDataAccessMock = new Mock<ICommunityModeratorsDataAccess>();
            _communityDataAccessMock = new Mock<ICommunityDataAccess>();

            _userContainer = new UserContainer(
                _userDataAccessMock.Object,
                _passwordHelperMock.Object,
                _authenticationSystemMock.Object,
                _profileDataAccessMock.Object,
                _userFriendsDataAccessMock.Object,
                _postDataAccessMock.Object,
                _commentDataAccessMock.Object,
                _messageDataAccessMock.Object,
                _communityMembersDataAccessMock.Object,
                _communityModeratorsDataAccessMock.Object,
                _communityDataAccessMock.Object
            );
        }

        [TestMethod]
        public void CreateAndSaveSignedUpUser_WhenUserNameIsUnique_CreatesAndSavesUser()
        {
            // Arrange
            string username = "testUser";
            string password = "testPassword";
            string email = "test@test.com";

            _userDataAccessMock.Setup(x => x.GetUserNames()).Returns(new List<string> { "abc"});
            _passwordHelperMock.Setup(x => x.GetSalt()).Returns("salt");
            _passwordHelperMock.Setup(x => x.GetHashedPassword(password, "salt")).Returns("hashedPassword");

            // Act
            _userContainer.CreateAndSaveSignedUpUser(username, password, email);

            // Assert
            _userDataAccessMock.Verify(x => x.SaveUser(It.IsAny<User>()), Times.Once);
            _profileDataAccessMock.Verify(x => x.CreateRecord(It.IsAny<Guid>(), username), Times.Once);
        }


        [TestMethod]
        public void CreateAndSaveSignedUpUser_WhenUserNameIsNotUnique_ThrowsUserCreationException()
        {
            // Arrange
            string username = "testUser";
            string password = "testPassword";
            string email = "test@test.com";

            _userDataAccessMock.Setup(x => x.GetUserNames()).Returns(new List<string> { "testUser" });

            // Act and Assert
            Assert.ThrowsException<UserCreationException>(() => _userContainer.CreateAndSaveSignedUpUser(username, password, email));
        }

        [TestMethod]
        public void ValidateCredentials_WithValidCredentials_ReturnsTrue()
        {
            // Arrange
            string username = "testUser";
            string password = "testPassword";
            string salt = "salt";
            string hashedPassword = "hashedPassword";

            _userDataAccessMock.Setup(x => x.GetSalt(username)).Returns(salt);
            _userDataAccessMock.Setup(x => x.GetPassword(username)).Returns(hashedPassword);
            _authenticationSystemMock.Setup(x => x.ValidateCredentials(username, password, salt, hashedPassword)).Returns(true);

            // Act
            bool isValid = _userContainer.ValidateCredentials(username, password);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateCredentials_WithInvalidCredentials_ReturnsFalse()
        {
            // Arrange
            string username = "testUser";
            string password = "InvalidtestPassword";
            string salt = "salt";
            string hashedPassword = "hashedPassword";

            _userDataAccessMock.Setup(x => x.GetSalt(username)).Returns(salt);
            _userDataAccessMock.Setup(x => x.GetPassword(username)).Returns(hashedPassword);
            _authenticationSystemMock.Setup(x => x.ValidateCredentials(username, password, salt, hashedPassword)).Returns(false);

            // Act
            bool isValid = _userContainer.ValidateCredentials(username, password);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void GetUserId_WithExistingUsername_ReturnsUserId()
        {
            // Arrange
            string username = "testUser";
            string userId = "12345";

            _userDataAccessMock.Setup(x => x.DoesUsernameExist(username)).Returns(true);
            _userDataAccessMock.Setup(x => x.GetUserId(username)).Returns(userId);

            // Act
            string result = _userContainer.GetUserId(username);

            // Assert
            Assert.AreEqual(userId, result);
        }

        [TestMethod]
        public void GetUserId_WithNonExistingUsername_ThrowsItemNotFoundException()
        {
            // Arrange
            string username = "nonExistingUser";

            _userDataAccessMock.Setup(x => x.DoesUsernameExist(username)).Returns(false);

            // Act and Assert
            Assert.ThrowsException<ItemNotFoundException>(() => _userContainer.GetUserId(username));
        }

        [TestMethod]
        public void GetUserName_WithExistingUserId_ReturnsUsername()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string username = "testUser";

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
            _userDataAccessMock.Setup(x => x.GetUserName(userId)).Returns(username);

            // Act
            string result = _userContainer.GetUserName(userId);

            // Assert
            Assert.AreEqual(username, result);
        }

        [TestMethod]
        public void GetUserName_WithNonExistingUserId_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(false);

            // Act and Assert
            Assert.ThrowsException<ItemNotFoundException>(() => _userContainer.GetUserName(userId));
        }

        [TestMethod]
        public void GetProfileDto_WithExistingUserId_ReturnsProfileDtoWithFriends()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            ProfileDto expectedProfileDto = new ProfileDto { UserId = userId };
            Guid friend1 = Guid.NewGuid();
            Guid friend2 = Guid.NewGuid();
            expectedProfileDto.Friends.Add(friend1);
            expectedProfileDto.Friends.Add(friend2);

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
            _profileDataAccessMock.Setup(x => x.LoadProfileRecord(userId)).Returns(expectedProfileDto);
            _userFriendsDataAccessMock.Setup(x => x.GetUserFriends(userId)).Returns(new List<Guid> { friend1, friend2 });

            // Act
            ProfileDto result = _userContainer.GetProfileDto(userId);

            // Assert
            Assert.AreEqual(expectedProfileDto, result);
            CollectionAssert.AreEqual(expectedProfileDto.Friends, result.Friends);
        }

        [TestMethod]
        public void GetProfileDto_WithNonExistingUserId_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(false);

            // Act and Assert
            Assert.ThrowsException<ItemNotFoundException>(() => _userContainer.GetProfileDto(userId));
        }

        [TestMethod]
        public void CheckIfUserIsFriends_ValidInputAndIsFriends_ReturnsTrue()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid friendId = Guid.NewGuid();

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
            _userDataAccessMock.Setup(x => x.DoesUserIdExist(friendId)).Returns(true);
            _userFriendsDataAccessMock.Setup(x => x.CheckRecordExists(userId, friendId)).Returns(true);

            // Act
            bool result = _userContainer.CheckIfUserIsFriends(userId, friendId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfUserIsFriends_ValidInputButNotFriends_ReturnsFalse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid friendId = Guid.NewGuid();

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
            _userDataAccessMock.Setup(x => x.DoesUserIdExist(friendId)).Returns(true);
            _userFriendsDataAccessMock.Setup(x => x.CheckRecordExists(userId, friendId)).Returns(false);

            // Act
            bool result = _userContainer.CheckIfUserIsFriends(userId, friendId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckIfUserIsFriends_WithInvalidUserId_ThrowsItemNotFoundException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid friendId = Guid.NewGuid();

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(false);

            // Act and Assert
            Assert.ThrowsException<ItemNotFoundException>(() => _userContainer.CheckIfUserIsFriends(userId, friendId));
        }
    }
}
