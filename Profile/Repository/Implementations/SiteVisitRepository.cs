using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class SiteVisitRepository : GenericRepository<SiteVisit>, ISiteVisitRepository
{
    private readonly ApplicationContext _context;

    public SiteVisitRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task IncrementVisitCountAsync()
    {
        var siteVisit = await _context.SiteVisits.FirstOrDefaultAsync();
        if (siteVisit == null)
        {
            siteVisit = new SiteVisit { VisitCount = 1 };
            await _context.SiteVisits.AddAsync(siteVisit);
        }
        else
        {
            siteVisit.VisitCount++;
        }
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetVisitCountAsync()
    {
        var siteVisit = await _context.SiteVisits.FirstOrDefaultAsync();
        return siteVisit?.VisitCount ?? 0;
    }
}