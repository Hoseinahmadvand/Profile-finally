using GoogleReCaptcha.V3.Interface;
using GoogleReCaptcha.V3;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Profile;
using Profile.Data;
using Profile.Repository.Implementations;
using Profile.Repository.Interfaces;
using Profile.Services.Implementations;
using Profile.Services.Interfaces;
using System.Globalization;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container
services.AddRazorPages();

// Authentication Configuration
services.AddAuthentication("Cookies").AddCookie(options =>
{
    options.LoginPath = "/Login";
});
services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin");
});
// Localization Configuration
services.AddLocalization(options => options.ResourcesPath = "SharedResource"); // Update path if necessary

var supportedCultures = new[]
{
    new CultureInfo("fa"),
    new CultureInfo("ar"),
    new CultureInfo("de")
};
services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("fa");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Database Connection
services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//ReCaptcha Service
services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

// Register Services
services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

services.AddCustomServices(); // Using extension method for DI configuration


builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 209715200; // 200 مگابایت
});
services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 209715200; 
});
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    SeedData.Initialize(serviceProvider);
}



app.UseStaticFiles();
app.UseRequestLocalization(); 
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();  
app.UseCookiePolicy();
app.MapRazorPages(); 


// Redirect root to Index
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Path: {context.Request.Path}");
    Console.WriteLine($"Request Method: {context.Request.Method}");
    await next();
});



app.Run();
