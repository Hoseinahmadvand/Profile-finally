using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.UserAgg;
using Profile.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Profile.Pages.Admin.UserPage
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        [Compare("Password", ErrorMessage = "password & ComfirmPassword no same")]
        public string ConfirmPasswors { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var user = new User()
                {
                    Username = UserName,
                    PasswordHash = HashPassword(Password),
                    Role = "Admin"
                };
                await _userService.AddAsync(user);
                TempData["SuccessMessage"] = "Operation completed successfully";
                return RedirectToPage("/admin/Common/Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Operation Failed";
                return RedirectToPage("/admin/Common/Index");
            }

        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var x = Convert.ToBase64String(bytes);
            return x;
        }
    }
}
