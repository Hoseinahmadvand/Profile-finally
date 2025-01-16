using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.SetingSiteAgg;
using Profile.Services.Interfaces;

namespace Profile.Pages.Admin.FooterMange;

public class EditModel : PageModel
{
    private readonly IFooterService _footerService;

    public EditModel(IFooterService footerService)
    {
        _footerService = footerService;
    }

    [BindProperty]
    public Footer Footer { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Footer = await _footerService.GetByIdAsync(id);

        if (Footer == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
       
        await _footerService.UpdateAsync(Footer);
        return RedirectToPage("Index");
    }
}

