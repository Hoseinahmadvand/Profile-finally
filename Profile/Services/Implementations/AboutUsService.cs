using Profile.Models.AboutUsAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;

namespace Profile.Services.Implementations;

public class AboutUsService : GenericService<AboutUs>, IAboutUsService
{
    private readonly IAboutUsRepository _aboutUsRepository;

    public AboutUsService(IAboutUsRepository aboutUsRepository) : base(aboutUsRepository)
    {
        _aboutUsRepository = aboutUsRepository;
    }

    public async Task<AboutUs> GetAboutUsWithTranslationsAsync()
    {
        return await _aboutUsRepository.GetAboutUsWithTranslationsAsync();
    }
}