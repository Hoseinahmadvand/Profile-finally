using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.SetingSiteAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class SiteSettingRepository : GenericRepository<SiteSetting>, ISiteSettingRepository
{
    private readonly ApplicationContext _context;

    public SiteSettingRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SiteSetting> GetSiteSettingsAsync()
    {
        return await _context.SiteSettings.FirstOrDefaultAsync();
    }
}
