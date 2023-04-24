using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Containers;
using SocialMedia.DataAccess;
using SocialMediaWebApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Containers
builder.Services.AddTransient<IPostContainer,PostContainer>();
builder.Services.AddTransient<IUserContainer, UserContainer>();
builder.Services.AddTransient<ICommunityContainer, CommunityContainer>();
//DataAccess
builder.Services.AddTransient<ICommunityDataAccess, CommunityDB>();
builder.Services.AddTransient<IPostDataAccess, PostDB>();
builder.Services.AddTransient<IUserDataAccess, UserDB>();
builder.Services.AddTransient<ICommunityMembersDataAccess, CommunityMembersDB>();
builder.Services.AddTransient<ICommunityRulesDataAccess, CommunityRulesDB>();
//other
builder.Services.AddTransient<IPasswordHelper, PasswordHelper>();
builder.Services.AddTransient<IAuthenticationSystem, AuthenticationSystem>();

//Authentication cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = new PathString("/Login");
    options.AccessDeniedPath = new PathString("/AccessDenied");
}
);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
