using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.ConnectUsAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.ConnectUs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IContactUsService _contactUsService;

        public EditModel(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string? Address { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string? FarsiDescription { get; set; }
        [BindProperty]
        public string? ArabicDescription { get; set; }
        [BindProperty]
        public string? GermanDescription { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var contactUs = await _contactUsService.GetContactUsWithTranslationsAsync(id);

            if (contactUs == null)
            {
                return NotFound();
            }

            Id = contactUs.Id;
            Address = contactUs.Address;
            Email = contactUs.Email;
            Phone = contactUs.Phone;

            var farsiTranslation = contactUs.Translations.FirstOrDefault(t => t.Language == "fa");
            if (farsiTranslation != null)
            {
                FarsiDescription = farsiTranslation.Description;
            }

            var arabicTranslation = contactUs.Translations.FirstOrDefault(t => t.Language == "ar");
            if (arabicTranslation != null)
            {
                ArabicDescription = arabicTranslation.Description;
            }

            var germanTranslation = contactUs.Translations.FirstOrDefault(t => t.Language == "de");
            if (germanTranslation != null)
            {
                GermanDescription = germanTranslation.Description;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try { 
            var contactUs = await _contactUsService.GetContactUsWithTranslationsAsync(Id);
            if (contactUs == null)
            {
                return NotFound();
            }

            contactUs.Address = "Address";
            contactUs.Email = Email;
            contactUs.Phone = Phone;

            UpdateOrAddTranslation(contactUs, "fa", "FarsiDescription");
            UpdateOrAddTranslation(contactUs, "ar", "ArabicDescription");
            UpdateOrAddTranslation(contactUs, "de", "GermanDescription");

            await _contactUsService.UpdateAsync(contactUs);

                TempData["SuccessMessage"] = "Operation completed successfully";
                return RedirectToPage("./Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Operation Failed";
                return RedirectToPage("./Index");
            }
        }

        private void UpdateOrAddTranslation(ContactUs contactUs, string language, string description)
        {
            var translation = contactUs.Translations.FirstOrDefault(t => t.Language == language);
            if (translation != null)
            {
                translation.Description = description;
            }
            else
            {
                contactUs.Translations.Add(new ContactUsTranslation
                {
                    Language = language,
                    Description = description
                });
            }
        }
    }
}
