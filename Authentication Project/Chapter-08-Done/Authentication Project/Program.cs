using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using StartcodeAuthentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

builder.Services.AddScoped<CustomCookieEvents>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/User/AccessDenied";
    o.LoginPath = "/User/Login";
    o.Cookie.Name = "MyAuthCookie";

    o.ExpireTimeSpan = TimeSpan.FromDays(7);
    o.SlidingExpiration = true;


    //o.Events = new CustomCookieEvents();
    o.EventsType = typeof(CustomCookieEvents);


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



//app.UseHttpsRedirection();

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


/*
 * 
    // Called when a challenge triggers a redirect to the login page.
    o.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            // For API requests, return 401 Unauthorized
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }
        else
        {
            // Redirect to login page for non-API requests
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        }
    };

    // Called after the cookie is issued
    o.Events.OnSignedIn = context =>
    {
        //Update last login time in database
        //userService.UpdateLastLogin(userId);

        return Task.CompletedTask;
    };


    o.Events.OnSigningIn = context =>
    {
        Console.WriteLine($"### SignIn.OnSigningIn");

        var identity = (ClaimsIdentity)context.Principal.Identity;
        identity.AddClaim(new Claim("MembershipPoints", "1234"));

        return Task.CompletedTask;
    };

    o.Events.OnSignedIn = context =>
    {
        Console.WriteLine($"### SignIn.OnSignedIn");
        return Task.CompletedTask;
    };

    o.Events.OnRedirectToReturnUrl = context =>
    {
        Console.WriteLine($"### SignIn.OnRedirectToReturnUrl");

        if(context.RedirectUri is not null)
           context.Response.Redirect(context.RedirectUri);
        

        return Task.CompletedTask;
    };

    o.Events.OnSignedIn = context =>
    {
        Console.WriteLine($"### SignIn.OnSignedIn");

        var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("SignInEvents");

        logger.LogInformation($"User {context.Principal.Identity?.Name} signed in");

        return Task.CompletedTask;
    };


    o.Events.OnCheckSlidingExpiration = context =>
    {
        Console.WriteLine();
        Console.WriteLine($"### OnCheckSlidingExpiration");
        Console.WriteLine($"### Time elapsed: {context.ElapsedTime}");
        Console.WriteLine($"### Time remaining: {context.RemainingTime}");
        Console.WriteLine($"### Should renew: {context.ShouldRenew}");
        Console.WriteLine();

        //Only renew before lunch
        if (DateTime.Now.Hour < 13)
            context.ShouldRenew = false;

        return Task.CompletedTask;
    };

    o.Events.OnValidatePrincipal = context =>
    {
        Console.WriteLine("### OnValidatePrincipal");


        switch (new Random().Next(0, 3))
        {
            case 0:
                Console.WriteLine("### Adding custom claim to current principal");

                var identity = (ClaimsIdentity)context.Principal.Identity;
                identity.AddClaim(new Claim("MembershipPoints", "1234"));
                break;

            case 1:
                Console.WriteLine("### Rejecting principal");
                context.RejectPrincipal();
                break;

            case 2:
                Console.WriteLine("### Replacing principal");

                var claims = new List<Claim>
            {
                new("name", "Alice"),
                new("email", "alice@example.com"),
                new("role", "Developer")
            };

                var newIdentity = new ClaimsIdentity(
                    claims,
                    authenticationType: "TestAuth",
                    nameType: "name",
                    roleType: "role");

                var newPrincipal = new ClaimsPrincipal(newIdentity);
                context.ReplacePrincipal(newPrincipal);
                break;
        }

        return Task.CompletedTask;
    };


    o.Events.OnSigningOut = context =>
    {
    Console.WriteLine($"### SignOut.OnSigningOut");

        context.Response.Headers.Add("X-Action", "LoggedOut");

        context.Response.Cookies.Append("UserSignedOut", DateTime.UtcNow.ToString());

        return Task.CompletedTask;
    };

    o.Events.OnRedirectToReturnUrl = context =>
    {
        Console.WriteLine($"### SignIn.OnRedirectToReturnUrl");

        context.Response.Redirect("/Home/Index");

        return Task.CompletedTask;
    };

*/
