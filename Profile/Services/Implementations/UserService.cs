using Profile.Models.UserAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
namespace Profile.Services.Implementations;

public class UserService : GenericService<User>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) : base(userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null || user.PasswordHash != HashPassword(password))
            return null;

        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var x = Convert.ToBase64String(bytes);
        return x;
    }
}
