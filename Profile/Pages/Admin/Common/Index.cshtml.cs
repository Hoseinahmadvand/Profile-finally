using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Scripting;
using Profile.Models;
using Profile.Models.UserAgg;
using Profile.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Profile.Pages.Admin.Common;

[Authorize(Roles = "Admin")]
[Authorize]
[Route("/admin")]
public class IndexModel : PageModel
{
    private readonly IVideoService _videoService;
    private readonly IProjectService _projectService;
    private readonly IGalleryImageService _galleryService;
    private readonly ISkillService _skillService;
    private readonly IUserService _userService;

    private readonly IWebHostEnvironment _environment;
    public IndexModel(
        IVideoService videoService,
        IProjectService projectService,
        IGalleryImageService galleryService,
        ISkillService skillService,
        ISiteSettingService siteSettingService,
        ISiteVisitService siteVisitService,
        IUserService userService,
        IWebHostEnvironment environment)
    {
        _videoService = videoService;
        _projectService = projectService;
        _galleryService = galleryService;
        _skillService = skillService;
        _userService = userService;
        _environment = environment;
    }


    public int TotalVideos { get; set; }
    public int TotalProjects { get; set; }
    public int TotalGalleryImages { get; set; }
    public int TotalSkills { get; set; }

    public IEnumerable<User> Users { get; set; }

    [BindProperty]
    public User User { get; set; }

    public async Task OnGetAsync()
    {
        TotalVideos = (await _videoService.GetAllAsync()).Count();
        TotalProjects = (await _projectService.GetAllAsync()).Count();
        TotalGalleryImages = (await _galleryService.GetGalleryImagesAsync()).Count();
        TotalSkills = (await _skillService.GetAllAsync()).Count();
        Users = await _userService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        
        if (User.Id == 0)
        {
            if(User.Username == null || User.PasswordHash==null)
                return Page();

            User.PasswordHash = HashPassword(User.PasswordHash);
            User.Role = "Admin";
            await _userService.AddAsync(User);
        }
        else
        {
            // ویرایش کاربر موجود
            var existingUser = await _userService.GetByIdAsync(User.Id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // به‌روزرسانی فیلدهای قابل ویرایش
            existingUser.Username = User.Username;

            // اگر رمز عبور جدیدی ارائه شد، آن را هش کنید
            if (!string.IsNullOrWhiteSpace(User.PasswordHash))
            {
                existingUser.PasswordHash = HashPassword(User.PasswordHash);
            }

            await _userService.UpdateAsync(existingUser);
        }

        return RedirectToPage();
    }


    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userService.DeleteAsync(user);
        return RedirectToPage();
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var x = Convert.ToBase64String(bytes);
        return x;
    }




}
