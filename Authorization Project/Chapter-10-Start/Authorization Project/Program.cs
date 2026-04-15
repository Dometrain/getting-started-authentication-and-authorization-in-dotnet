using Microsoft.AspNetCore.Mvc.Razor;
using StartcodeAuthorization.Features.Invoices;
using StartcodeAuthorization.Features.Users;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(o =>
{
    o.RequireAuthenticatedSignIn = true;   
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/users/AccessDenied";
    o.LoginPath = "/users/login";
});


builder.Services.AddAuthorization(o =>
{
    // Config

    //o.AddPolicy("finance", policy =>
    //    policy.RequireRole("finance")
    //          .RequireAuthenticatedUser());

    o.AddPolicy("finance", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            var titleOk = user.HasClaim(claim => claim.Type == "JobTitle" &&
                                                 claim.Value == "finance");

            var countryOk = user.HasClaim(claim => claim.Type == "country" &&
                                                  (claim.Value == "Sweden" ||
                                                   claim.Value == "Denmark"));

            var rolesOk = user.IsInRole("finance") ||
                          user.IsInRole("management");

            return rolesOk && titleOk && countryOk;
        }));

    o.AddPolicy("sales", policy =>
        policy.RequireRole("manager", "sales")
              .RequireClaim("country", ["Sweden", "Denmark","Norway"]));
            
      

    o.AddPolicy("admin", policy =>
        policy.RequireRole("admin")
              .RequireRole("management"));          //AND

    o.AddPolicy("customer", policy =>
            policy.RequireAuthenticatedUser());

});








builder.Services.AddSingleton<IUserDatabase, UserDatabase>();
builder.Services.AddSingleton<IInvoiceDatabase, InvoiceDatabase>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
