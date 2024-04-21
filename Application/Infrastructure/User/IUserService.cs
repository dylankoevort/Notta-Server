using Models;

namespace Infrastructure;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(string userId);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(string userId);
}