using Profile.Models.AboutUsAgg;
namespace Profile.Services.Interfaces;

public interface IAboutUsService : IGenericService<AboutUs>
{
    Task<AboutUs> GetAboutUsWithTranslationsAsync();
}



