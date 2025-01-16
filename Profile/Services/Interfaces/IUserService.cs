using Profile.Models.UserAgg;
namespace Profile.Services.Interfaces;

public interface IUserService:IGenericService<User>
{
    Task<User> AuthenticateAsync(string username, string password);
}



