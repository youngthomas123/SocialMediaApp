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

        public RegularUserForm(IServiceProvider serviceProvider, IUser LoggedInUser)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userContainer = _serviceProvider.GetService<IUserContainer>();
            _messageContainer = _serviceProvider.GetService<IMessageContainer>();


        }
    }
}
