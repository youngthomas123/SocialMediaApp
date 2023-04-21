
using BCrypt.Net;
using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;



// Start DI Container
var services = new ServiceCollection();

services.AddTransient<ICommunityDataAccess, CommunityDB>();
services.AddTransient<ICommunityMembersDataAccess, CommunityMembersDB>();
services.AddTransient<ICommunityRulesDataAccess, CommunityRulesDB>();
services.AddTransient<IUserDataAccess, UserDB>();
services.AddTransient<IPasswordHelper, PasswordHelper>();
services.AddTransient<IPostDataAccess, PostDB>();

services.AddTransient<IUserContainer, UserContainer>();
services.AddTransient<ICommunityContainer, CommunityContainer>();
services.AddTransient<CommunityContainer>();
services.AddTransient<UserContainer>();
services.AddTransient<PasswordHelper>();


var serviceProvider = services.BuildServiceProvider();
// End DI Container


UserContainer userContainer = serviceProvider.GetService<UserContainer>();

User user = new User("Thomas", "Password", "Thomas@gmail.com");




userContainer.SaveUser(user);