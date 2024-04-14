using Models;

namespace Infrastructure;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User GetUserById(int? userId, string? userUid);
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int? userId, string? userUid);
}