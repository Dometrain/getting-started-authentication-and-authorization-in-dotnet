using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<MyAuthorizationFilter>();
    o.Filters.Add<MyResourceFilter>();
    o.Filters.Add<MyActionFilter>();
    o.Filters.Add<MyResultFilter>();
    o.Filters.Add<MyExceptionFilter>();
});


builder.Services.AddAuthentication().AddCookie();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine($"@@@ - before UseAuthentication - {context.Request.Path}");
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine("@@@ - after UseAuthorization");
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();





public class MyAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine("### Authorization Filter: Checking authorization...");
    }
}

public class MyResourceFilter : IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine(" ### Resource Filter: BEFORE resource execution");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine(" ### Resource Filter: AFTER resource execution");
    }
}

public class MyActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("  ### Action Filter: BEFORE action execution");
        Console.WriteLine($"  ### Action: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("   ### Action Filter: AFTER action execution");
    }
}

public class MyResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("    ### Result Filter: BEFORE result execution");
        Console.WriteLine($"    ### Result Type: {context.Result?.GetType().Name}");

        // Modify response before it's sent
        context.HttpContext.Response.Headers.Add("X-Custom-Header", "MyValue");

        // Access the action result
        if (context.Result is ObjectResult objectResult)
        {
            // Modify the data being returned
            objectResult.Value = new { OriginalData = objectResult.Value, Timestamp = DateTime.UtcNow };
        }

    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("    ### Result Filter: AFTER result execution");
    }
}

public class MyExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Console.WriteLine();
        Console.WriteLine("§§§ Exception Filter: Handling exception");
        Console.WriteLine($"§§§ Exception: '{context.Exception.Message}'");
        Console.WriteLine();
    }
}
