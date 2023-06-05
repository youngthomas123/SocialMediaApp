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
using System.Xml.Linq;

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

            foreach(var username in _userContainer.GetAllUsernames())
            {
                CreateCommunityAddModsCB.Items.Add(username);
            }

            ModsInCreatedCommunityLiB.Items.Add(User.UserName);

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
            var communityName = CreateCommunityNameTB.Text;
            var description = CreateCommunityDescriptionRTB.Text;

            List<string>Rules = new List<string>();

            var rule1 = CreateRule1TB.Text;
            var rule2 = CreateRule2TB.Text;
            var rule3 = CreateRule3TB.Text;


            if (!string.IsNullOrEmpty(rule1))
            {
                Rules.Add(rule1);
            }

            if(!string.IsNullOrEmpty(rule2))
            {
                Rules.Add(rule2);
            }

            if(!string.IsNullOrEmpty(rule3))
            {
                Rules.Add(rule3);
            }
            
            List<string> Mods = new List<string>();

            foreach(var item in ModsInCreatedCommunityLiB.Items)
            {
                Mods.Add((string)item);
            }

            try
            {
                _communityContainer.CreateAndSaveCommunity(User.UserId, communityName, description, Rules, Mods);
            }
            catch(ItemNotFoundException)
            {
                MessageBox.Show("Mod not found");
            }
            catch(InvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(AccessException)
            {
                MessageBox.Show("You dont have access to create community");
            }

            CreateCommunityNameTB.Clear();
            CreateCommunityDescriptionRTB.Clear();
            CreateRule1TB.Clear();
            CreateRule2TB.Clear();
            CreateRule3TB.Clear();
            ModsInCreatedCommunityLiB.Items.Clear();


        }

        private void CreateCommunityAddModBT_Click(object sender, EventArgs e)
        {
            
            var selectedUser = (string)CreateCommunityAddModsCB.SelectedItem;
            if(!string.IsNullOrEmpty(selectedUser))
            {
                if(ModsInCreatedCommunityLiB.Items.Count <= 2 && !ModsInCreatedCommunityLiB.Items.Contains(selectedUser))
                {
                    ModsInCreatedCommunityLiB.Items.Add(selectedUser);
                 
                }
                else
                {
                    MessageBox.Show("Cannot add mod");
                }
            }
            
            
        }

        private void CreateCommunityRemoveModBT_Click(object sender, EventArgs e)
        {
            var selectedUser = ModsInCreatedCommunityLiB.SelectedItem;

            ModsInCreatedCommunityLiB.Items.Remove(selectedUser);

        }
    }
}
