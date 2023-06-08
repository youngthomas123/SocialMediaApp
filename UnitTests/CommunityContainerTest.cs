using Moq;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class CommunityContainerTest
    {
        [TestClass]
        public class CommunityContainerTests
        {
            private Mock<ICommunityDataAccess> _communityDataAccessMock;
            private Mock<ICommunityMembersDataAccess> _communityMembersDataAccessMock;
            private Mock<ICommunityRulesDataAccess> _communityRulesDataAccessMock;
            private Mock<IPostDataAccess> _postDataAccessMock;
            private Mock<IUserDataAccess> _userDataAccessMock;
            private Mock<ICommunityModeratorsDataAccess> _communityModeratorsDataAccessMock;
            private CommunityContainer _communityContainer;

            [TestInitialize]
            public void Initialize()
            {
                _communityDataAccessMock = new Mock<ICommunityDataAccess>();
                _communityMembersDataAccessMock = new Mock<ICommunityMembersDataAccess>();
                _communityRulesDataAccessMock = new Mock<ICommunityRulesDataAccess>();
                _postDataAccessMock = new Mock<IPostDataAccess>();
                _userDataAccessMock = new Mock<IUserDataAccess>();
                _communityModeratorsDataAccessMock = new Mock<ICommunityModeratorsDataAccess>();
                _communityContainer = new CommunityContainer(
                    _communityDataAccessMock.Object,
                    _communityMembersDataAccessMock.Object,
                    _communityRulesDataAccessMock.Object,
                    _postDataAccessMock.Object,
                    _userDataAccessMock.Object,
                    _communityModeratorsDataAccessMock.Object);
            }

            [TestMethod]
            public void CreateAndSaveCommunity_ValidInput_CreatesCommunityAndRulesAndModerators()
            {
                // Arrange
                Guid userId = Guid.NewGuid();
                string communityName = "MyCommunity";
                string description = "Community description";
                List<string> rules = new List<string> { "Rule 1", "Rule 2" };
                List<string> mods = new List<string> { "Mod1", "Mod2" };

                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
                _userDataAccessMock.Setup(x => x.IsUserPremium(userId)).Returns(true);
                _userDataAccessMock.Setup(x => x.DoesUsernameExist(It.IsAny<string>())).Returns(true);
                _userDataAccessMock.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());

                _communityDataAccessMock.Setup(x => x.GetCommunityNames()).Returns(new List<string>{"ews", "fdv"});
                _communityDataAccessMock.Setup(x => x.SaveCommunity(It.IsAny<Community>()));
                _communityRulesDataAccessMock.Setup(x => x.CreateRule(It.IsAny<Guid>(), It.IsAny<string>()));
                _communityModeratorsDataAccessMock.Setup(x => x.CreateRecord(It.IsAny<Guid>(), It.IsAny<Guid>()));

                // Act
                _communityContainer.CreateAndSaveCommunity(userId, communityName, description, rules, mods);

                // Assert
                _communityDataAccessMock.Verify(x => x.SaveCommunity(It.IsAny<Community>()), Times.Once);
                _communityRulesDataAccessMock.Verify(x => x.CreateRule(It.IsAny<Guid>(), It.IsAny<string>()), Times.Exactly(2));
                _communityModeratorsDataAccessMock.Verify(x => x.CreateRecord(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Exactly(2));
            }

            [TestMethod]
            public void CreateAndSaveCommunity_ValidInputButUserIsNotPremium_ThrowsAccessException()
            {
                // Arrange
                Guid userId = Guid.NewGuid();
                string communityName = "MyCommunity";
                string description = "Community description";
                List<string> rules = new List<string> { "Rule 1", "Rule 2" };
                List<string> mods = new List<string> { "Mod1", "Mod2" };

                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
                _userDataAccessMock.Setup(x => x.IsUserPremium(userId)).Returns(false);
                _communityDataAccessMock.Setup(x => x.GetCommunityNames()).Returns(new List<string> { "ews", "fdv" });
             
                // Act & Assert
                Assert.ThrowsException<AccessException>(() =>
                    _communityContainer.CreateAndSaveCommunity(userId, communityName, description, rules, mods));
            }

            [TestMethod]
            [DataRow("", "Description")]
            [DataRow("CommnuityName", "")]
            [DataRow("", "")]
            [DataRow(null, "")]
            [DataRow("", null)]
            [DataRow(null, null)]
            [DataRow("communityName", null)]
            [DataRow(null, "Description")]
            public void CreateAndSaveCommunity_InvalidCommunityInput_ThrowsInvalidInputException(string communityName, string description)
            {
                // Arrange
                Guid userId = Guid.NewGuid();
                List<string> rules = new List<string> { "Rule 1", "Rule 2" };
                List<string> mods = new List<string> { "Mod1", "Mod2" };

                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
                _userDataAccessMock.Setup(x => x.IsUserPremium(userId)).Returns(true);
       
                _communityDataAccessMock.Setup(x => x.GetCommunityNames()).Returns(new List<string> { "ews", "fdv" });

                // Act & Assert
                Assert.ThrowsException<InvalidInputException>(() =>
                    _communityContainer.CreateAndSaveCommunity(userId, communityName, description, rules, mods));


            }

            [TestMethod]
            public void FollowCommunity_ValidInput_CallsCommunityMember()
            {
                // Arrange
                Guid communityId = Guid.NewGuid();
                Guid userId = Guid.NewGuid();

                _communityDataAccessMock.Setup(x => x.DoesCommunityIdExist(communityId)).Returns(true);
                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
                _communityMembersDataAccessMock.Setup(x => x.CheckRecordExists(communityId, userId)).Returns(false);
                _communityMembersDataAccessMock.Setup(x => x.CreateMember(communityId, userId));

                // Act
                _communityContainer.FollowCommunity(communityId, userId);

                // Assert
                _communityDataAccessMock.Verify(x => x.DoesCommunityIdExist(communityId), Times.Once);
                _userDataAccessMock.Verify(x => x.DoesUserIdExist(userId), Times.Once);
                _communityMembersDataAccessMock.Verify(x => x.CheckRecordExists(communityId, userId), Times.Once);
                _communityMembersDataAccessMock.Verify(x => x.CreateMember(communityId, userId), Times.Once);
            }

            [TestMethod]
            public void FollowCommunity_ValidInputButUserAlreadyFollowsCommunity_ThrowsAccessException()
            {
                // Arrange
                Guid communityId = Guid.NewGuid();
                Guid userId = Guid.NewGuid();

                _communityDataAccessMock.Setup(x => x.DoesCommunityIdExist(communityId)).Returns(true);
                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
                _communityMembersDataAccessMock.Setup(x => x.CheckRecordExists(communityId, userId)).Returns(true);

                // Act & Assert
                Assert.ThrowsException<AccessException>(() =>
                    _communityContainer.FollowCommunity(communityId, userId));

       
            }

            [TestMethod]
            public void FollowCommunity_InvalidUserIdOrCommunityId_ThrowsItemNotFoundException()
            {
                // Arrange
                Guid communityId = Guid.NewGuid();
                Guid userId = Guid.NewGuid();

                _communityDataAccessMock.Setup(x => x.DoesCommunityIdExist(communityId)).Returns(false);
                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(false);

                // Act & Assert
                Assert.ThrowsException<ItemNotFoundException>(() =>
                    _communityContainer.FollowCommunity(communityId, userId));
            }

            [TestMethod]
            public void UnfollowCommunity_ValidInput_CallsDeleteMember()
            {
                // Arrange
                Guid communityId = Guid.NewGuid();
                Guid userId = Guid.NewGuid();

                _communityDataAccessMock.Setup(x => x.DoesCommunityIdExist(communityId)).Returns(true);
                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
                _communityMembersDataAccessMock.Setup(x => x.CheckRecordExists(communityId, userId)).Returns(true);
                _communityMembersDataAccessMock.Setup(x => x.DeleteMember(communityId, userId));

                // Act
                _communityContainer.UnfollowCommunity(communityId, userId);

                // Assert
                _communityMembersDataAccessMock.Verify(x => x.DeleteMember(communityId, userId), Times.Once);
            }

            [TestMethod]
            public void UnfollowCommunity_ValidInputButUserDoesNotFollowTheCommunity_ThrowAccessException()
            {
                // Arrange
                Guid communityId = Guid.NewGuid();
                Guid userId = Guid.NewGuid();

                _communityDataAccessMock.Setup(x => x.DoesCommunityIdExist(communityId)).Returns(true);
                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(true);
                _communityMembersDataAccessMock.Setup(x => x.CheckRecordExists(communityId, userId)).Returns(false);

                // Act & Assert
                Assert.ThrowsException<AccessException>(() =>
                    _communityContainer.UnfollowCommunity(communityId, userId));
            }

            [TestMethod]
            public void UnfollowCommunity_InvalidUserNameOrCommunityId_ThrowsItemNotFoundException()
            {
                // Arrange
                Guid communityId = Guid.NewGuid();
                Guid userId = Guid.NewGuid();

                _communityDataAccessMock.Setup(x => x.DoesCommunityIdExist(communityId)).Returns(false);
                _userDataAccessMock.Setup(x => x.DoesUserIdExist(userId)).Returns(false);


                // Act & Assert
                Assert.ThrowsException<ItemNotFoundException>(() =>
                    _communityContainer.UnfollowCommunity(communityId, userId));
            }


        }


    }
}
