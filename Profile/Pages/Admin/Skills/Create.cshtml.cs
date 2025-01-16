// Pages/Admin/Skills/Create.cshtml.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Models.SkillAgg;
using Profile.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Profile.Pages.Admin.Skills
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ISkillService _skillService;

        public CreateModel(ISkillService skillService)
        {
            _skillService = skillService;
        }


        [BindProperty]
        public int Percent { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public Language lang{ get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var skill = new Skill
                {

                    Persent = Percent,
                    Title = Title,
                    lang = lang
                };

                await _skillService.AddAsync(skill);
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
