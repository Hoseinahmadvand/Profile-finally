using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.UserAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}