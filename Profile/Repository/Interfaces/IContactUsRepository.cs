using Profile.Models.ConnectUsAgg;

namespace Profile.Repository.Interfaces;

public interface IContactUsRepository : IGenericRepository<ContactUs>
{
    Task<ContactUs> GetContactUsWithTranslationsAsync(int id);
    Task<ContactUs> GetContactUsWithTranslationsAsync();
}

