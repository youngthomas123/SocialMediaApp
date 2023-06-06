using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.DataAccess;
using SocialMediaWebApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using SocialMedia.BusinessLogic.Algorithms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Containers
builder.Services.AddTransient<IPostContainer,PostContainer>();
builder.Services.AddTransient<IUserContainer, UserContainer>();
builder.Services.AddTransient<ICommunityContainer, CommunityContainer>();
builder.Services.AddTransient<ICommentContainer, CommentContainer>();
builder.Services.AddTransient<IMessageContainer, MessageContainer>();
//DataAccess
builder.Services.AddTransient<ICommunityDataAccess, CommunityDB>();
builder.Services.AddTransient<IPostDataAccess, PostDB>();
builder.Services.AddTransient<IUserDataAccess, UserDB>();
builder.Services.AddTransient<ICommunityMembersDataAccess, CommunityMembersDB>();
builder.Services.AddTransient<ICommunityRulesDataAccess, CommunityRulesDB>();
builder.Services.AddTransient<ICommentDataAccess, CommentDB>();
builder.Services.AddTransient<IUpvotedPostsDataAccess, UpvotedPostsDB>();
builder.Services.AddTransient<IDownvotedPostsDataAccess, DownvotedPostsDB>();
builder.Services.AddTransient<IUpvotedCommentsDataAccess, UpvotedCommentsDB>();
builder.Services.AddTransient<IDownvotedCommentsDataAccess, DownvotedCommentsDB>();
builder.Services.AddTransient<IProfileDataAccess, ProfileDB>();
builder.Services.AddTransient<IUserFriendsDataAccess, UserFriendsDB>();
builder.Services.AddTransient<IReportedPostsDataAccess, ReportedPostsDB>();
builder.Services.AddTransient<IReportedCommentsDataAccess, ReportedCommentsDB>();
builder.Services.AddTransient<ICommunityModeratorsDataAccess, CommunityModeratorsDB>();
builder.Services.AddTransient<IReportReasonsDataAccess, ReportReasonsDB>();
builder.Services.AddTransient<IMessageDataAccess, MessageDB>();
builder.Services.AddTransient<IRemovedPostsDataAccess, RemovedPostsDB>();
builder.Services.AddTransient<IRemovedCommentsDataAccess, RemovedCommentsDB>();
//other
builder.Services.AddTransient<IPasswordHelper, PasswordHelper>();
builder.Services.AddTransient<IAuthenticationSystem, AuthenticationSystem>();
builder.Services.AddTransient<IContentFilterAndRanking, ContentFilterAndRanking>();
builder.Services.AddSingleton<BotModerator>();

//Authentication cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = new PathString("/Login");
    options.AccessDeniedPath = new PathString("/AccessDenied");
}
);
//This means that any page or endpoint without explicit authorization requirements will require authenticated users
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});


var app = builder.Build();

var botModerator = app.Services.GetService<BotModerator>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

_ = botModerator.StartModerationAsync();

app.Run();
