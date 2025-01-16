// Pages/Admin/AboutUs/Index.cshtml.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.AboutUsAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.AboutUss
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IAboutUsService _aboutUsService;

        public IndexModel(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        public AboutUs AboutUs { get; set; }

        public async Task OnGetAsync()
        {
            AboutUs = await _aboutUsService.GetAboutUsWithTranslationsAsync();
            if (AboutUs == null)
            {
                AboutUs=new AboutUs() { 
                };
            }
        }
    }
}
