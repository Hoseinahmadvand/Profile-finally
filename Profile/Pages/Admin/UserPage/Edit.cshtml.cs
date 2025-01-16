using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.UserAgg;
using Profile.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Profile.Pages.Admin.UserPage
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            Id = user.Id;
            UserName = user.Username;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userService.GetByIdAsync(Id);
            if (user == null)
                return NotFound();

            user.Username = UserName;

            if (!string.IsNullOrEmpty(Password))
            {
                if (Password != ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match");
                    return Page();
                }
                user.PasswordHash = HashPassword(Password);
            }

            await _userService.UpdateAsync(user);
            TempData["SuccessMessage"] = "Admin updated successfully";
            return RedirectToPage("/admin/Common/Index");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

}
