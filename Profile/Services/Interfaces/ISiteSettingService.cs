using Profile.Models.SetingSiteAgg;
namespace Profile.Services.Interfaces;

public interface ISiteSettingService : IGenericService<SiteSetting>
{
    Task<SiteSetting> GetSiteSettingsAsync();
}



