using Profile.Models.ConnectUsAgg;
namespace Profile.Services.Interfaces;

public interface IContactUsService : IGenericService<ContactUs>
{
    Task<ContactUs> GetContactUsWithTranslationsAsync(int id);  
    Task<ContactUs> GetContactUsWithTranslationsAsync();
}



