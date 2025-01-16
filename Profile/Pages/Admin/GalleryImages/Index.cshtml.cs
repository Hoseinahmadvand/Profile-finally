using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.GalleryAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.GalleryImages;
[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IGalleryImageService _galleryImageService;
    private readonly IWebHostEnvironment _environment;

    public IndexModel(IGalleryImageService galleryImageService, IWebHostEnvironment environment)
    {
        _galleryImageService = galleryImageService;
        _environment = environment;
    }
    [BindProperty]
    public List<GalleryImage> GalleryImages { get; set; }

    public async Task OnGetAsync()
    {
        GalleryImages = (await _galleryImageService.GetGalleryImagesAsync()).ToList();
    }
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var image = await _galleryImageService.GetByIdAsync(id);
        if (image == null)
        {
            return NotFound();
        }

        // حذف فایل تصویر از سرور
        if (!string.IsNullOrEmpty(image.ImagePath))
        {
            var filePath = Path.Combine(_environment.WebRootPath, image.ImagePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        // حذف رکورد تصویر از دیتابیس
        await _galleryImageService.DeleteAsync(image);

        return RedirectToPage();
    }
}
