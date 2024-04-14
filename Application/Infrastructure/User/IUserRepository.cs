using Models;

namespace Infrastructure;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User GetUserById(int userId);
    User GetUserByUid(string userUid);
    void AddUser(User user);
    void UpdateUser();
    void DeleteUser(int userId);
}