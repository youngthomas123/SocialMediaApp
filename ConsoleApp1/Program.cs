
using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;



// Start DI Container
var services = new ServiceCollection();
services.AddTransient<ICommentDataAcess, CommentDB>();
services.AddTransient<CommentContainer>();
var serviceProvider = services.BuildServiceProvider();
// End DI Container




























