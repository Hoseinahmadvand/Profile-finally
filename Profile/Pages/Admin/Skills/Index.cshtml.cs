// Pages/Admin/Skills/Index.cshtml.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.SkillAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Skills;
[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ISkillService _skillService;

    public IndexModel(ISkillService skillService)
    {
        _skillService = skillService;
    }

    public List<Skill> Skills { get; set; }

    public async Task OnGetAsync()
    {
        var skills = await _skillService.GetAllAsync();
        Skills = skills.ToList();

   
    
    } 
    public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var skill = await _skillService.GetByIdAsync(id);
            if (skill != null)
            {
                await _skillService.DeleteAsync(skill);
            }

            return RedirectToPage();
        }
}

public class SkillTranslationViewModel
{
    public int SkillId { get; set; }

    public int Percent { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
}
