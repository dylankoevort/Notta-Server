using Models;

namespace Infrastructure;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User GetUserById(int userId);
    User GetUserById(string userUid);
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int userId);
    void DeleteUser(string userUid);
}