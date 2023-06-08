using Moq;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class MessageContainerTest
    {
        private Mock<IMessageDataAccess> _messageDataAccessMock;
        private Mock<IUserDataAccess> _userDataAccessMock;
        private MessageContainer _messageContainer;

        [TestInitialize]
        public void Initialize()
        {
            _messageDataAccessMock = new Mock<IMessageDataAccess>();
            _userDataAccessMock = new Mock<IUserDataAccess>();
            _messageContainer = new MessageContainer(_messageDataAccessMock.Object, _userDataAccessMock.Object);
        }

        [TestMethod]
        public void CreateAndSaveMessage_ValidInput_CreatesAndSavesMessage()
        {
            // Arrange
            string subject = "Test Subject";
            string body = "Test Body";
            Guid senderId = Guid.NewGuid();
            Guid recipientId = Guid.NewGuid();

            Message message = new Message(subject, body, senderId, recipientId);

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(senderId)).Returns(true);
            _userDataAccessMock.Setup(x => x.DoesUserIdExist(recipientId)).Returns(true);
            _messageDataAccessMock.Setup(x => x.SaveMessage(message));

            // Act
            _messageContainer.CreateAndSaveMessage(subject, body, senderId, recipientId);

            // Assert
            _userDataAccessMock.Verify(x => x.DoesUserIdExist(senderId), Times.Once);
            _userDataAccessMock.Verify(x => x.DoesUserIdExist(recipientId), Times.Once);
            _messageDataAccessMock.Verify(x => x.SaveMessage(It.IsAny<Message>()), Times.Once);
        }

        [TestMethod]
        [DataRow("", "body")]
        [DataRow("subject", "")]
        [DataRow("", "")]
        [DataRow(null, "")]
        [DataRow("", null)]
        [DataRow(null, "body")]
        [DataRow(null,null)]
        public void CreateAndSaveMessage_InvalidMessageInput_ThrowsInvalidInputException(string subject, string body)
        {
            Guid senderId = Guid.NewGuid();
            Guid recipientId = Guid.NewGuid();

            Message message = new Message(subject, body, senderId, recipientId);

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(senderId)).Returns(true);
            _userDataAccessMock.Setup(x => x.DoesUserIdExist(recipientId)).Returns(true);
            _messageDataAccessMock.Setup(x => x.SaveMessage(message));

            // Act & Assert
            Assert.ThrowsException<InvalidInputException>(() =>
            {
                _messageContainer.CreateAndSaveMessage(subject, body, senderId, recipientId);
            });
        }

        [TestMethod]
        public void CreateAndSaveMessage_WhenSenderOrRecipientIdDoesNotExist_ThrowsItemNotFoundException()
        {
            string subject = "Test Subject";
            string body = "Test Body";
            Guid senderId = Guid.NewGuid();
            Guid recipientId = Guid.NewGuid();

            Message message = new Message(subject, body, senderId, recipientId);

            _userDataAccessMock.Setup(x => x.DoesUserIdExist(senderId)).Returns(false);
            _userDataAccessMock.Setup(x => x.DoesUserIdExist(recipientId)).Returns(false);

            // Act & Assert
            Assert.ThrowsException<ItemNotFoundException>(() =>
            {
                _messageContainer.CreateAndSaveMessage(subject, body, senderId, recipientId);
            });
        }
    }
}
