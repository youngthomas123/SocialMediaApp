
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;




var services = new ServiceCollection();
services.AddTransient<ICommentDataAcess, CommentDB>();
services.AddTransient<CommentContainer>();
var serviceProvider = services.BuildServiceProvider();

var commentContainer = serviceProvider.GetService<CommentContainer>();
var loadedComments = commentContainer.GetComments();




