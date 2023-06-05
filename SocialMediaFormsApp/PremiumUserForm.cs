using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaFormsApp
{
    public partial class PremiumUserForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserContainer _userContainer;
        private readonly IMessageContainer _messageContainer;
        private readonly ICommunityContainer _communityContainer;


        private PremiumUser User { get; set; }

        public PremiumUserForm(IServiceProvider serviceProvider, IUser LoggedInUser)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userContainer = _serviceProvider.GetService<IUserContainer>();
            _messageContainer = _serviceProvider.GetService<IMessageContainer>();
            _communityContainer = _serviceProvider.GetService<ICommunityContainer>();

            User = LoggedInUser as PremiumUser;
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void PremiumUserForm_Load(object sender, EventArgs e)
        {
            UsernameLB.Text = User.UserName;
            EmailLB.Text = User.Email;
            DateCreatedLB.Text = Convert.ToString(User.DateCreated);
            GenderLB.Text = User.Gender ?? "Null";
            LocationLB.Text = User.Location ?? "Null";

            foreach (var friend in User.UserFriends)
            {
                FriendsLiB.Items.Add(friend);
            }

            foreach (var followingCommuniy in User.UserFollowingCommunities)
            {
                FollowingCommunitiesLiB.Items.Add(followingCommuniy);
            }

            foreach (var UserModeratingCommunity in User.UserModeratingCommunities)
            {
                ModForLiB.Items.Add(UserModeratingCommunity);
            }

            foreach (var message in User.ReceivedMessages)
            {
                ReceivedMessagesLiB.Items.Add(message);
            }

            foreach(var createdCommunity in User.UserCreatedCommunities)
            {
                CreatedCommunitiesLiB.Items.Add(createdCommunity);
            }


        }

        private void ViewMessageBT_Click(object sender, EventArgs e)
        {
            var SelectedMessage = (SocialMedia.BusinessLogic.Message)ReceivedMessagesLiB.SelectedItem;


            MessageBox.Show($"Date : {Convert.ToString(SelectedMessage.DateCreated)} \nFrom : {_userContainer.GetUserName(SelectedMessage.SenderId)} \nSubject : {SelectedMessage.Subject} \nBody : {SelectedMessage.Body}");
        }

        private void SendMessageBT_Click(object sender, EventArgs e)
        {
            var RecipientName = ToTB.Text;
            var Subject = SubjectTB.Text;
            var Body = MessageBodyRTB.Text;


            try
            {
                var RecipientId = new Guid(_userContainer.GetUserId(RecipientName));

                _messageContainer.CreateAndSaveMessage(Subject, Body, User.UserId, RecipientId);
            }
            catch (ItemNotFoundException)
            {
                MessageBox.Show("Invalid Username");
            }
            catch (InvalidInputException)
            {
                MessageBox.Show("Invalid Input");
            }

            ToTB.Clear();
            SubjectTB.Clear();
            MessageBodyRTB.Clear();
        }

        private void CreateCommunityBT_Click(object sender, EventArgs e)
        {

        }

        private void CreateCommunityAddModBT_Click(object sender, EventArgs e)
        {

        }

        private void CreateCommunityRemoveModBT_Click(object sender, EventArgs e)
        {

        }
    }
}
