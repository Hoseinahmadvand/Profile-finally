using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.SetingSiteAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class FooterRepository : GenericRepository<Footer>, IFooterRepository
{
    public FooterRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Footer>> GetFooterItemAsync()
    {
       return await _context.Set<Footer>().ToListAsync();
    }
}
