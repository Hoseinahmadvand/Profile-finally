using Profile.Models.AboutUsAgg;

namespace Profile.Repository.Interfaces;

public interface IAboutUsRepository : IGenericRepository<AboutUs>
{
    Task<AboutUs> GetAboutUsWithTranslationsAsync();
}


