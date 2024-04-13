using Infrastructure;
using Models;

namespace Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User GetUserById(int userId)
    {
        return _userRepository.GetUserById(userId);
    }
    
    public User GetUserById(string userUid)
    {
        return _userRepository.GetUserById(userUid);
    }

    public void AddUser(User user)
    {
        var dbUser = GetUserById(user.UserId);
        
        if (dbUser == null)
        {
            dbUser = GetUserById(user.UserUid);
        }
        
        if (dbUser != null)
        {
            throw new Exception("User already exists");
        }
        
        _userRepository.AddUser(user);
    }

    public void UpdateUser(User user)
    {
        var dbUser = GetUserById(user.UserUid);
        
        if (dbUser == null)
        {
            throw new Exception("User does not exist");
        }
        
        var updatedUser = new User
        {
            UserId = dbUser.UserId,
            UserUid = user.UserUid,
            UserName = user.UserName,
            UserEmail = user.UserEmail
        };
        
        _userRepository.UpdateUser(updatedUser);
    }

    public void DeleteUser(int userId)
    {
        _userRepository.DeleteUser(userId);
    }
    
    public void DeleteUser(string userUid)
    {
        _userRepository.DeleteUser(userUid);
    }
}