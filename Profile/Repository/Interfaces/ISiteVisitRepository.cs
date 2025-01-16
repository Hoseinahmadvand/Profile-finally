using Profile.Models;

namespace Profile.Repository.Interfaces;

public interface ISiteVisitRepository : IGenericRepository<SiteVisit>
{
    Task IncrementVisitCountAsync();
    Task<int> GetVisitCountAsync();
}

