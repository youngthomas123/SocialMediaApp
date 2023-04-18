using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.DataAccess;
using SocialMediaWebApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IPostContainer,PostContainer>();
builder.Services.AddTransient<IUserContainer, UserContainer>();
builder.Services.AddTransient<ICommunityContainer, CommunityContainer>();
builder.Services.AddTransient<ICommunityDataAccess, CommunityDB>();
builder.Services.AddTransient<IPostDataAccess, PostDB>();
builder.Services.AddTransient<IUserDataAccess, UserDB>();
builder.Services.AddTransient<ICommunityMembersDataAccess, CommunityMembersDB>();
builder.Services.AddTransient<ICommunityRulesDataAccess, CommunityRulesDB>();




var app = builder.Build();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
