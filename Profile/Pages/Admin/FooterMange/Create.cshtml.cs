using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.SetingSiteAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.FooterMange;

public class CreateModel : PageModel
{
    private readonly IFooterService _footerService;

    public CreateModel(IFooterService footerService)
    {
        _footerService = footerService;
    }

    [BindProperty]
    public Footer Footer { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _footerService.AddAsync(Footer);
        return RedirectToPage("Index");
    }
}


