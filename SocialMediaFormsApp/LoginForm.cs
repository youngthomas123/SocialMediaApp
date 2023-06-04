using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Custom_exception;
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
    public partial class LoginForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserContainer _userContainer;

        public LoginForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userContainer = _serviceProvider.GetService<IUserContainer>();
        }

        private void LoginBT_Click(object sender, EventArgs e)
        {
            string username = UsernameTB.Text;
            string password = PasswordTB.Text;  

            var isUserValid = _userContainer.ValidateCredentials(username, password);

            if (isUserValid)
            {
                try
                {
                    var User = _userContainer.GetUserByName(username);

                    if (User is PremiumUser)
                    {
                        
                        this.Hide();

                        //Show PremiumUserForm
                        using (PremiumUserForm PremiumUserForm = new PremiumUserForm(_serviceProvider, User))
                        {
                            PremiumUserForm.FormClosed += PremiumUserForm_Closed;
                            PremiumUserForm.ShowDialog();
                        }

                    }
                    else if (User is RegularUser)
                    {
                        
                        this.Hide();

                        //Show RegularUserForm

                        using (RegularUserForm RegularUserForm = new RegularUserForm(_serviceProvider, User))
                        {
                            RegularUserForm.FormClosed += RegularUserForm_Closed;
                            RegularUserForm.ShowDialog();
                        }

                    }
                }
                catch(ItemNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
            else
            {
                MessageBox.Show("Incorrect username or password");
            }
            UsernameTB.Clear();
            PasswordTB.Clear();

           
        }
        private void PremiumUserForm_Closed(object sender, FormClosedEventArgs e)
        {
            // Show the LoginForm again 
            this.Show();
        }

        private void RegularUserForm_Closed(object sender, FormClosedEventArgs e)
        {
            // Show the LoginForm again 
            this.Show();
        }
    }
}
