using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Models.VideoAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Videos;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IVideoService _videoService;

    public IndexModel(IVideoService videoService)
    {
        _videoService = videoService;
    }
    public List<Video> Videos { get; set; }
    public Language Language { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }

    public async Task OnGetAsync()
    {
        var videos = await _videoService.GetAllAsync();
        Videos=videos.ToList();
      
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var video = await _videoService.GetByIdAsync(id);
        if (video != null)
        {
            await _videoService.DeleteAsync(video);
        }

        return RedirectToPage();
    }
}


