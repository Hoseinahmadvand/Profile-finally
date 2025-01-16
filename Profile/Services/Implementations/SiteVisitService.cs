using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class SiteVisitService : ISiteVisitService
{
    private readonly ISiteVisitRepository _siteVisitRepository;

    public SiteVisitService(ISiteVisitRepository siteVisitRepository)
    {
        _siteVisitRepository = siteVisitRepository;
    }

    public async Task IncrementVisitCountAsync()
    {
        await _siteVisitRepository.IncrementVisitCountAsync();
    }

    public async Task<int> GetVisitCountAsync()
    {
        return await _siteVisitRepository.GetVisitCountAsync();
    }
}
