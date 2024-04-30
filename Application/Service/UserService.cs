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
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<User> GetUserById(string userId)
    {
        return await _userRepository.GetUserById(userId);
    }
    
    public async Task<User> CreateUser(User user)
    {
        return await _userRepository.CreateUser(user);
    }

    public async Task<User> UpdateUser(User user)
    {
        return await _userRepository.UpdateUser(user);
    }

    public async Task DeleteUser(string userId)
    {
        await _userRepository.DeleteUser(userId);
    }
}