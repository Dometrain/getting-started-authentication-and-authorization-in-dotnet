using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
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

app.Use(async (context, next) =>
{
    //1. Load the claims for this user 
    var myClaims = new List<Claim>()
       {
           new("sub","1234"),
           new("name","Bob"),
           new("email","bob@tn-data.se"),
           new("role","developer"),
           new("role","sales"),
           new("role","admin")
       };

    var myIdentity = new ClaimsIdentity(claims: myClaims,
                                        authenticationType: "custom",
                                        nameType: "name",
                                        roleType: "role");

    var myPrincipal = new ClaimsPrincipal(myIdentity);

    context.User = myPrincipal;

    // call the next middleware 
    await next.Invoke();
});


app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
