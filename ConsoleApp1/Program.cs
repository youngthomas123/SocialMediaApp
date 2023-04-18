
using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
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
services.AddTransient<IPostDataAccess, PostDB>();
services.AddTransient<ICommunityContainer, CommunityContainer>();
services.AddTransient<CommunityContainer>();



var serviceProvider = services.BuildServiceProvider();
// End DI Container


CommunityContainer communityContainer = serviceProvider.GetService<CommunityContainer>();

var communities =  communityContainer.LoadCompleteCommunityDtos();

int x = 0;















