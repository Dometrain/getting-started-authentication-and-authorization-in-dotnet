using CustomAuth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{ 
    o.AccessDeniedPath = "/User/AccessDenied";
    o.LoginPath = "/User/Login";
    o.Cookie.Name = "MyAuthCookie";
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();




//app.Use(async (context, next) =>
//{
//    context.User = myPrincipal;

//    // call the next middleware 
//    await next.Invoke();
//});