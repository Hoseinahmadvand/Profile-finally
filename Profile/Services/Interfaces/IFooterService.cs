using Profile.Models.SetingSiteAgg;
namespace Profile.Services.Interfaces;

public interface IFooterService : IGenericService<Footer>
{
    Task<IEnumerable<Footer>> GetFooterItemAsync();
}
