using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.AboutUsAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class AboutUsRepository : GenericRepository<AboutUs>, IAboutUsRepository
{
    public AboutUsRepository(ApplicationContext context) : base(context) { }

    public async Task<AboutUs> GetAboutUsWithTranslationsAsync()
    {
        return await _context.Set<AboutUs>()
            .Include(a => a.Translations)
            .FirstOrDefaultAsync();
    }
}

