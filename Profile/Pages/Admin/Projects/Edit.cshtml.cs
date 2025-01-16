// Pages/Admin/Projects/Edit.cshtml.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Models.ProjectAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Projects
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg", ".webp", ".tiff" };

        public EditModel(IProjectService projectService, IWebHostEnvironment environment)
        {
            _projectService = projectService;
            _environment = environment;
        }

        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        [BindProperty]
        public string ImagePath { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public Language lang { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {

            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            ProjectId = project.Id;
            ImagePath = project.ImageName;
            Description = project.Description;
            Title = project.Title;
            lang = project.lang;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
               

                var pro = await _projectService.GetAllAsync();
                var project = pro.Where(p => p.Id == ProjectId).FirstOrDefault();

                if (project == null)
                {
                    return NotFound();
                }

                if (ImageFile != null)
                {
                    if (!_allowedExtensions.Contains(Path.GetExtension(ImageFile.FileName).ToLowerInvariant()))
                    {
                        TempData["ErrorMessage"] = "File Invalid file type. Allowed formats: JPG, JPEG, PNG, GIF, BMP, SVG, WEBP, TIFF.";

                        return Page();
                    }
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/projects");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var imagePath = Path.Combine("uploads/projects/", ImageFile.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, imagePath);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    if (!string.IsNullOrEmpty(project.ImageName))
                    {
                        var oldFilePath = Path.Combine(_environment.WebRootPath, project.ImageName);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    project.ImageName = imagePath;
                }
                project.Title=Title;
                project.Description=Description;
                project.lang=lang;
                await _projectService.UpdateAsync(project);

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
