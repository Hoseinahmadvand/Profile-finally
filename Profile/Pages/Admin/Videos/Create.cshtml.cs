using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Models.VideoAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Videos;

[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly IVideoService _videoService;
    private readonly IWebHostEnvironment _environment;
    private readonly string[] _allowedExtensions = { ".mp4", ".webm", ".ogg", ".mkv" };

    public CreateModel(IVideoService videoService, IWebHostEnvironment environment)
    {
        _videoService = videoService;
        _environment = environment;
    }

    [BindProperty]
    public Language Language { get; set; }
    [BindProperty]
    public string Title { get; set; }
    [BindProperty]
    public string Description { get; set; }
    [BindProperty]
    public IFormFile File { get; set; }

    public string Url { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {

        if (File == null || !_allowedExtensions.Contains(Path.GetExtension(File.FileName).ToLowerInvariant()))
        {
            TempData["ErrorMessage"] = " Invalid file type. Only MP4, WebM, OGG, and MKV are allowed.";
            return Page();
        }

        if (File != null)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/videos");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var videoPath = Path.Combine("uploads/videos/", File.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, videoPath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await File.CopyToAsync(fileStream);
            }

            Url = videoPath;
        }


        var video = new Video
        {
            CreateTime = DateTime.Now,
            lang = Language,
            Title = Title,
            Description = Description,
            Url = Url

        };

        await _videoService.AddAsync(video);
        TempData["SuccessMessage"] = "Operation completed successfully";
        return RedirectToPage("./Index");
    }
}

