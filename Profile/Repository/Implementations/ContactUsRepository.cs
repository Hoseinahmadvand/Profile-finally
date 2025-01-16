using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.ConnectUsAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class ContactUsRepository : GenericRepository<ContactUs>, IContactUsRepository
{
    private readonly ApplicationContext _context;

    public ContactUsRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ContactUs> GetContactUsWithTranslationsAsync(int id)
    {
        return await _context.ContactUs
            .Include(c => c.Translations)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ContactUs> GetContactUsWithTranslationsAsync()
    {
       return await _context.ContactUs.Include(c => c.Translations).FirstOrDefaultAsync();
    }
}
