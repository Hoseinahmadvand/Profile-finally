using Profile.Models.ConnectUsAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class ContactUsService : GenericService<ContactUs>, IContactUsService
{
    private readonly IContactUsRepository _contactUsRepository;

    public ContactUsService(IContactUsRepository contactUsRepository) : base(contactUsRepository)
    {
        _contactUsRepository = contactUsRepository;
    }

    public async Task<ContactUs> GetContactUsWithTranslationsAsync(int id)
    {
        return await _contactUsRepository.GetContactUsWithTranslationsAsync(id);
    }

    public async Task<ContactUs> GetContactUsWithTranslationsAsync()
    {
        return await _contactUsRepository.GetContactUsWithTranslationsAsync();
    }
}
