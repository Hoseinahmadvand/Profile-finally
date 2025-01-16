using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Services.Interfaces;
using System.Security.Claims;

namespace Profile.Pages;

public class LoginModel : PageModel
{
    private readonly IUserService _userService;

    public LoginModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userService.AuthenticateAsync(Username, Password);
        if (user == null)
        {
            ErrorMessage = "Invalid username or password";
            return Page();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, "login");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(principal);

        return RedirectToPage("/Admin/common/Index"); // صفحه اصلی مدیریت
    }
      public async Task<IActionResult> OnGetLogoutAsync()
    {
        await HttpContext.SignOutAsync();

        
        // هدایت به صفحه Login
        return RedirectToPage("/Index");
    }
}
