using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.ConnectUsAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.ConnectUs
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IContactUsService _contactUsService;

        public IndexModel(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        public ContactUs ContactInfo { get; set; }
        public string FarsiDescription { get; set; }
        public string ArabicDescription { get; set; }
        public string GermanDescription { get; set; }

        public async Task OnGetAsync()
        {
            ContactInfo = await _contactUsService.GetContactUsWithTranslationsAsync(1); // فرض می‌کنیم ID=1

            var farsiTranslation = ContactInfo.Translations.FirstOrDefault(t => t.Language == "fa");
            if (farsiTranslation != null)
            {
                FarsiDescription = farsiTranslation.Description;
            }

            var arabicTranslation = ContactInfo.Translations.FirstOrDefault(t => t.Language == "ar");
            if (arabicTranslation != null)
            {
                ArabicDescription = arabicTranslation.Description;
            }

            var germanTranslation = ContactInfo.Translations.FirstOrDefault(t => t.Language == "de");
            if (germanTranslation != null)
            {
                GermanDescription = germanTranslation.Description;
            }
        }
    }
}
