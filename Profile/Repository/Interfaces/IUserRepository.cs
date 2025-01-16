using Profile.Models.UserAgg;

namespace Profile.Repository.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUserByUsernameAsync(string username);
}

