
namespace Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Profile.Data;
using System.Linq;
using System.Threading.Tasks;

public class IpBlockMiddleware
{
    private readonly RequestDelegate _next;

    public IpBlockMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // مسیر فعلی درخواست
        var path = context.Request.Path.Value;

        // فقط صفحه اصلی (Index) بررسی شود
        if (path == "/")
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();

            if (clientIp != null)
            {
                // ایجاد Scope برای دستیابی به DbContext
                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                    // بررسی اینکه آیا IP کاربر در پایگاه داده مسدود شده است
                    if (dbContext.BlockedIps.Any(ip => ip.IpAddress == clientIp))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Access Denied");
                        return;
                    }
                }
            }
        }

        await _next(context);
    }
}




