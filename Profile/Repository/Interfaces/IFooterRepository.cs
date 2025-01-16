using Profile.Models.SetingSiteAgg;

namespace Profile.Repository.Interfaces;

public interface IFooterRepository : IGenericRepository<Footer>
{
    Task<IEnumerable<Footer>> GetFooterItemAsync();
}