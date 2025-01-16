// Pages/Admin/AboutUs/Edit.cshtml.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.AboutUsAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.AboutUss
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IAboutUsService _aboutUsService;

        public EditModel(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        [BindProperty]
        public DateTime Founded { get; set; }
       
        [BindProperty]
        public string FarsiTitle { get; set; }
        [BindProperty]
        public string FarsiDescription { get; set; }
        [BindProperty]
        public string ArabicTitle { get; set; }
        [BindProperty]
        public string ArabicDescription { get; set; }
        [BindProperty]
        public string GermanTitle { get; set; }
        [BindProperty]
        public string GermanDescription { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var aboutUs = await _aboutUsService.GetAboutUsWithTranslationsAsync();

            if (aboutUs == null)
            {
                return NotFound();
            }

            Founded = aboutUs.Founded;
        

            foreach (var translation in aboutUs.Translations)
            {
                switch (translation.Language)
                {
                    case "fa":
                        FarsiTitle = translation.Title;
                        FarsiDescription = translation.Description;
                        break;
                    case "ar":
                        ArabicTitle = translation.Title;
                        ArabicDescription = translation.Description;
                        break;
                    case "de":
                        GermanTitle = translation.Title;
                        GermanDescription = translation.Description;
                        break;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var aboutUs = await _aboutUsService.GetAboutUsWithTranslationsAsync();

                if (aboutUs == null)
                {
                    return NotFound();
                }

                aboutUs.Founded = Founded;
         

                aboutUs.Translations = new List<AboutUsTranslation>
            {
                new AboutUsTranslation { Language = "fa", Title = FarsiTitle, Description = FarsiDescription },
                new AboutUsTranslation { Language = "ar", Title = ArabicTitle, Description = ArabicDescription },
                new AboutUsTranslation { Language = "de", Title = GermanTitle, Description = GermanDescription }
            };

                await _aboutUsService.UpdateAsync(aboutUs);

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
