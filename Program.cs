using Microsoft.EntityFrameworkCore;
using PROG6212.Data;
using PROG6212.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the ClaimService
builder.Services.AddScoped<IClaimService, ClaimService>();

// Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Adjust for your database provider


// Configure logging services
builder.Logging.ClearProviders();
builder.Logging.AddConsole();  // Optionally log to the console

var app = builder.Build();

// Middleware to handle global exceptions
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke(); // Continue to the next middleware
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An unhandled exception occurred.");
        context.Response.Redirect("/Home/Error");  // Redirect to custom error page
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // Default error handler
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


// Default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();