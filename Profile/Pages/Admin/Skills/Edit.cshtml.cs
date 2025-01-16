// Pages/Admin/Skills/Edit.cshtml.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.Common;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.Skills
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ISkillService _skillService;

        public EditModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [BindProperty]
        public int SkillId { get; set; }
        [BindProperty]
        public int Percent { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public Language lang { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            var skill = await _skillService.GetByIdAsync(id);


            if (skill == null)
            {
                return NotFound();
            }

            SkillId = skill.Id;
            Percent = skill.Persent;
            Title = skill.Title;
            lang = skill.lang;


            return Page();
        }
   
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            try
            {
                var ski = await _skillService.GetAllAsync();
                var skill = ski.Where(s => s.Id == SkillId).FirstOrDefault();

                if (skill == null)
                {
                    return NotFound();
                }

                skill.Persent = Percent;
                skill.Title = Title;
                skill.lang = lang;


                await _skillService.UpdateAsync(skill);

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
