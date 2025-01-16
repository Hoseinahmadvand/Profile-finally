using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.ProjectAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Projects;
[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IProjectService _projectService;
    private readonly IWebHostEnvironment _environment;

    public IndexModel(IProjectService projectService, IWebHostEnvironment environment)
    {
        _projectService = projectService;
        _environment = environment;
    }

    public List<Project> Projects { get; set; }

    public async Task OnGetAsync()
    {
        var projects = await _projectService.GetAllAsync();
        Projects = projects.ToList();

 
    }
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        // حذف فایل تصویر از سرور
        if (!string.IsNullOrEmpty(project.ImageName))
        {
            var filePath = Path.Combine(_environment.WebRootPath, project.ImageName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        // حذف رکورد پروژه از دیتابیس
        await _projectService.DeleteAsync(project);

        return RedirectToPage();
    }
}

public class ProjectTranslationViewModel
{
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
    public string ImagePath { get; set; } // مسیر عکس
}
