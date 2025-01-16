
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models.SetingSiteAgg;
using Profile.Services.Interfaces;


namespace Profile.Pages.Admin.FooterMange;


public class IndexModel : PageModel
{
    private readonly IFooterService _footerService;

    public IndexModel(IFooterService footerService)
    {
        _footerService = footerService;
    }

    public IEnumerable<Footer> Footers { get; set; }

    public async Task OnGetAsync()
    {
        Footers = await _footerService.GetFooterItemAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var footer = await _footerService.GetByIdAsync(id);
        await _footerService.DeleteAsync(footer);
        return RedirectToPage(); 
    }
}

