using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.GalleryImages
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IGalleryImageService _galleryImageService;
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg", ".webp", ".tiff" };

        public EditModel(IGalleryImageService galleryImageService, IWebHostEnvironment environment)
        {
            _galleryImageService = galleryImageService;
            _environment = environment;
        }

        [BindProperty] public int Id { get; set; }
        [BindProperty] public string ImagePath { get; set; }
        [BindProperty] public IFormFile ImageFile { get; set; }
        [BindProperty] public Language lang { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var galleryImage = await _galleryImageService.GetByIdAsync(id);
            if (galleryImage == null) return NotFound();

            Id = galleryImage.Id;
            ImagePath = galleryImage.ImagePath;
            lang = galleryImage.lang;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid) return Page();
          
            var galleryImage = await _galleryImageService.GetByIdAsync(Id);
            if (galleryImage == null) return NotFound();

            if (ImageFile != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(ImageFile.FileName).ToLowerInvariant()))
                {
                    TempData["ErrorMessage"] = "File Invalid file type. Allowed formats: JPG, JPEG, PNG, GIF, BMP, SVG, WEBP, TIFF.";
                    return Page();
                }
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/gallery");
                Directory.CreateDirectory(uploadsFolder);
                var newImagePath = Path.Combine("uploads/gallery/", ImageFile.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, newImagePath);

                // حذف فایل قبلی
                if (!string.IsNullOrEmpty(galleryImage.ImagePath))
                {
                    var oldFilePath = Path.Combine(_environment.WebRootPath, galleryImage.ImagePath);
                    if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);
                }

                // ذخیره فایل جدید
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(fileStream);

                galleryImage.ImagePath = newImagePath;
            }

            galleryImage.lang = lang;

            await _galleryImageService.UpdateAsync(galleryImage);
            TempData["SuccessMessage"] = "Image updated successfully.";
            return RedirectToPage("./Index");
        }

    }
}
