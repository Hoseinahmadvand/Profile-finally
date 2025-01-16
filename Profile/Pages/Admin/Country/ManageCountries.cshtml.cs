using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models;

public class ManageCountriesModel : PageModel
{
    private readonly ApplicationContext _dbContext;

    public ManageCountriesModel(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<BlockedIp> BlockedIps { get; set; }

    [BindProperty]
    public string NewIpAddress { get; set; }

    public void OnGet()
    {
        BlockedIps = _dbContext.BlockedIps.ToList();
    }

    public IActionResult OnPostAdd()
    {
        if (!string.IsNullOrEmpty(NewIpAddress))
        {
            _dbContext.BlockedIps.Add(new BlockedIp { IpAddress = NewIpAddress });
            _dbContext.SaveChanges();
        }
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int id)
    {
        var blockedIp = _dbContext.BlockedIps.Find(id);
        if (blockedIp != null)
        {
            _dbContext.BlockedIps.Remove(blockedIp);
            _dbContext.SaveChanges();
        }
        return RedirectToPage();
    }
}
