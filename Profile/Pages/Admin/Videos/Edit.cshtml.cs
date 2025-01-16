using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Videos;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly IVideoService _videoService;
    private readonly IWebHostEnvironment _environment;
    private readonly string[] _allowedExtensions = { ".mp4", ".webm", ".ogg", ".mkv" };
    public EditModel(IVideoService videoService, IWebHostEnvironment environment)
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
    [BindProperty]
    public string Url { get; set; }
    public int VideoId { get; set; }
    private string _videoUrl;
    public async Task<IActionResult> OnGetAsync(int id)
    {
        var video = await _videoService.GetByIdAsync(id);

        if (video == null)
        {
            return NotFound();
        }
        _videoUrl = video.Url;
        VideoId = video.Id;
        Language = video.lang;
        Title = video.Title;
        Description = video.Description;
        Url = video.Url;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {

      
        var video = await _videoService.GetByIdAsync(id);

        if (video == null)
        {
            return NotFound();
        }


        if (File != null)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(File.FileName).ToLowerInvariant()))
            {
                TempData["ErrorMessage"] = " Invalid file type. Only MP4, WebM, OGG, and MKV are allowed.";
                return Page();
            }
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


        video.Title = Title;
        video.Description = Description;
        video.Url = Url;
        video.lang = Language;


        await _videoService.UpdateAsync(video);

        TempData["SuccessMessage"] = "Operation completed successfully";
        return RedirectToPage("./Index");
    }

}
