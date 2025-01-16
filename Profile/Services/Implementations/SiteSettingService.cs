using Profile.Models.SetingSiteAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class SiteSettingService : GenericService<SiteSetting>, ISiteSettingService
{
    private readonly ISiteSettingRepository _siteSettingRepository;

    public SiteSettingService(ISiteSettingRepository siteSettingRepository) : base(siteSettingRepository)
    {
        _siteSettingRepository = siteSettingRepository;
    }

    public async Task<SiteSetting> GetSiteSettingsAsync()
    {
        return await _siteSettingRepository.GetSiteSettingsAsync();
    }
}

