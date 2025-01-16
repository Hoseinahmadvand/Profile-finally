using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Models.ProjectAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Projects
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg", ".webp", ".tiff" };

        public CreateModel(IProjectService projectService, IWebHostEnvironment environment)
        {
            _projectService = projectService;
            _environment = environment;
        }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        [BindProperty] public string Title { get; set; }
        [BindProperty] public string Description { get; set; }
        [BindProperty] public Language lang { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile == null || !_allowedExtensions.Contains(Path.GetExtension(ImageFile.FileName).ToLowerInvariant()))
            {
                TempData["ErrorMessage"] ="File Invalid file type. Allowed formats: JPG, JPEG, PNG, GIF, BMP, SVG, WEBP, TIFF.";

                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string imagePath = null;

                if (ImageFile != null)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/projects");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    imagePath = Path.Combine("uploads/projects/", ImageFile.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, imagePath);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }
                }

                var project = new Project
                {
                    CreateTime = DateTime.Now,
                    ImageName = imagePath,  // مسیر فایل عکس
                    Description = Description,
                    Title = Title,
                    lang = lang
                };

                await _projectService.AddAsync(project);
                TempData["SuccessMessage"] = "Operation completed successfully";
                return RedirectToPage("./Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Operation Failed";
                return RedirectToPage("./Index");
            }
        }
    }
}
