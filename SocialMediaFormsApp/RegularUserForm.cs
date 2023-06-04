using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
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
    public partial class RegularUserForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserContainer _userContainer;
        private readonly IMessageContainer _messageContainer;
        private readonly ICommunityContainer _communityContainer;

        private IUser User { get; set; }

        public RegularUserForm(IServiceProvider serviceProvider, IUser LoggedInUser)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userContainer = _serviceProvider.GetService<IUserContainer>();
            _messageContainer = _serviceProvider.GetService<IMessageContainer>();
            _communityContainer = _serviceProvider.GetService<ICommunityContainer>();
            User = LoggedInUser;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void RegularUserForm_Load(object sender, EventArgs e)
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
        }

        private void SendMessageBT_Click(object sender, EventArgs e)
        {
            
        }

        private void ViewMessageBT_Click(object sender, EventArgs e)
        {

        }
    }
}
