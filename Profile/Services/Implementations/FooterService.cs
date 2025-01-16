using Profile.Models.SetingSiteAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class FooterService : GenericService<Footer>, IFooterService
{
    private readonly IFooterRepository _footerRepository;

    public FooterService(IFooterRepository footerRepository):base(footerRepository) 
    {
        _footerRepository = footerRepository;
    }

    public async Task<IEnumerable<Footer>> GetFooterItemAsync()
    {
        return await _footerRepository.GetFooterItemAsync();
    }
}



