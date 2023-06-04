using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.DataAccess;
using System;

namespace SocialMediaFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            //DI
            var services = new ServiceCollection();

            //Containers
            services.AddTransient<IUserContainer, UserContainer>();
            services.AddTransient<ICommunityContainer, CommunityContainer>();
            services.AddTransient<IMessageContainer, MessageContainer>();


            //DataAccess
            services.AddTransient<IUserDataAccess, UserDB>();
            services.AddTransient<IProfileDataAccess, ProfileDB>();
            services.AddTransient<IUserFriendsDataAccess, UserFriendsDB>();

            services.AddTransient<ICommunityDataAccess, CommunityDB>();
            services.AddTransient<ICommunityMembersDataAccess, CommunityMembersDB>();
            services.AddTransient<ICommunityRulesDataAccess, CommunityRulesDB>();
            services.AddTransient<IPostDataAccess, PostDB>();
            services.AddTransient<ICommunityModeratorsDataAccess, CommunityModeratorsDB>();

            services.AddTransient<IMessageDataAccess, MessageDB>();

            services.AddTransient<ICommentDataAccess, CommentDB>();

            //Other
            services.AddTransient<IPasswordHelper, PasswordHelper>();
            services.AddTransient<IAuthenticationSystem, AuthenticationSystem>();


           

            var serviceProvider = services.BuildServiceProvider();
            //End DI


           
            

            Application.Run(new LoginForm(serviceProvider));
        }
    }
}