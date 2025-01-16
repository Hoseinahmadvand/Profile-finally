using Profile.Models.SetingSiteAgg;

namespace Profile.Repository.Interfaces;

public interface ISiteSettingRepository : IGenericRepository<SiteSetting>
{
    Task<SiteSetting> GetSiteSettingsAsync();
}

