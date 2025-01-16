using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.SetingSiteAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.setting
{
    public class SettingsModel : PageModel
    {

        private readonly ISiteSettingService _siteSettingService;

        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg", ".webp", ".tiff" };


        public SettingsModel(ISiteSettingService siteSettingService, IWebHostEnvironment environment)
        {
            _siteSettingService = siteSettingService;
            _environment = environment;
        }


        [BindProperty] public SiteSetting SiteSettings { get; set; }
        [BindProperty] public IFormFile? ImageFile { get; set; }
        public async void OnGet()
        {

            SiteSettings = await _siteSettingService.GetSiteSettingsAsync() ?? new SiteSetting();
        }
        public async Task<IActionResult> OnPostAsync()
        {
           

            var existingSettings = await _siteSettingService.GetSiteSettingsAsync();


            if (ImageFile != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(ImageFile.FileName).ToLowerInvariant()))
                {
                    TempData["ErrorMessage"] = "File Invalid file type. Allowed formats: JPG, JPEG, PNG, GIF, BMP, SVG, WEBP, TIFF.";

                    return Page();
                }
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var newImagePath = Path.Combine("uploads", ImageFile.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, newImagePath);

                // حذف فایل قدیمی
                if (!string.IsNullOrEmpty(existingSettings.LogoPath))
                {
                    var oldFilePath = Path.Combine(_environment.WebRootPath, existingSettings.LogoPath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // ذخیره فایل جدید
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                existingSettings.LogoPath = newImagePath;
            }


            if (existingSettings == null)
            {
                await _siteSettingService.AddAsync(SiteSettings);
            }
            else
            {
                existingSettings.ShowSkillsFa = SiteSettings.ShowSkillsFa;
                existingSettings.ShowProjectsFa = SiteSettings.ShowProjectsFa;
                existingSettings.ShowGalleryFa = SiteSettings.ShowGalleryFa;
                existingSettings.ShowContactUsFa = SiteSettings.ShowContactUsFa;
                
                existingSettings.ShowSkillsAr = SiteSettings.ShowSkillsAr;
                existingSettings.ShowProjectsAr = SiteSettings.ShowProjectsAr;
                existingSettings.ShowGalleryAr = SiteSettings.ShowGalleryAr;
                existingSettings.ShowContactUsAr = SiteSettings.ShowContactUsAr; 
                
                existingSettings.ShowSkillsDe = SiteSettings.ShowSkillsDe;
                existingSettings.ShowProjectsDe = SiteSettings.ShowProjectsDe;
                existingSettings.ShowGalleryDe = SiteSettings.ShowGalleryDe;
                existingSettings.ShowContactUsDe = SiteSettings.ShowContactUsDe;

                existingSettings.TitleSiteFa = SiteSettings.TitleSiteFa;
                existingSettings.TitleSiteAr = SiteSettings.TitleSiteAr;
                existingSettings.TitleSiteDe = SiteSettings.TitleSiteDe;
                await _siteSettingService.UpdateAsync(existingSettings);
            }
            TempData["SuccessMessage"] = "Operation completed successfully";
            return RedirectToPage();
        }

    }
}
