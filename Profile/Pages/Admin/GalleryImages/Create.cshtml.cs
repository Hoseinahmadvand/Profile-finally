using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Models.GalleryAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.GalleryImages;

[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly IGalleryImageService _galleryImageService;
    private readonly IWebHostEnvironment _environment;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg", ".webp", ".tiff" };


    public CreateModel(IGalleryImageService galleryImageService, IWebHostEnvironment environment)
    {
        _galleryImageService = galleryImageService;
        _environment = environment;
    }


    [BindProperty] public IFormFile ImageFile { get; set; }
    [BindProperty] public Language lang { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        if (ImageFile == null || !_allowedExtensions.Contains(Path.GetExtension(ImageFile.FileName).ToLowerInvariant()))
        {
            TempData["ErrorMessage"] = "File Invalid file type. Allowed formats: JPG, JPEG, PNG, GIF, BMP, SVG, WEBP, TIFF.";

            return Page();
        }

        string imagePath = null;
        if (ImageFile != null)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/gallery");
            Directory.CreateDirectory(uploadsFolder);
            imagePath = Path.Combine("uploads/gallery/", ImageFile.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, imagePath);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await ImageFile.CopyToAsync(fileStream);
        }

        var galleryImage = new GalleryImage
        {
            ImagePath = imagePath,
            ImageName = ImageFile.FileName,
            Alt = ImageFile.Name,
            lang = lang
        };

        await _galleryImageService.AddAsync(galleryImage);
        TempData["SuccessMessage"] = "Image added successfully.";
        return RedirectToPage("./Index");
    }

}
