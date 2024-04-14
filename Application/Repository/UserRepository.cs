using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

public IEnumerable<User> GetAllUsers()
{
    try
    {
        return _context.Users.ToList();
    }
    catch (Exception ex)
    {
        // logger.LogError(ex, "An error occurred while getting all users");
        Console.WriteLine(ex.Message);
        throw;
    }
}

public User GetUserById(int userId)
{
    try
    {
        return _context.Users.FirstOrDefault(u => u.UserId == userId);
    }
    catch (Exception ex)
    {
        // logger.LogError(ex, "An error occurred while getting user by ID");
        Console.WriteLine(ex.Message);
        throw;
    }
}

public User GetUserByUid(string userUid)
{
    try
    {
        return _context.Users.FirstOrDefault(u => u.UserUid.Equals(userUid));
    }
    catch (Exception ex)
    {
        // logger.LogError(ex, "An error occurred while getting user by UID");
        Console.WriteLine(ex.Message);
        throw;
    }
}

public void AddUser(User user)
{
    try
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    catch (Exception ex)
    {
        // logger.LogError(ex, "An error occurred while adding user");
        Console.WriteLine(ex.Message);
        throw;
    }
}

public void UpdateUser()
{
    try
    {
        _context.SaveChanges();
    }
    catch (Exception ex)
    {
        // logger.LogError(ex, "An error occurred while updating user");
        Console.WriteLine(ex.Message);
        throw;
    }
}

public void DeleteUser(int userId)
{
    try
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        else
        {
            // User not found, handle the situation
            // For example, throw an exception or log a message
            throw new InvalidOperationException($"User with ID {userId} not found");
        }
    }
    catch (Exception ex)
    {
        // logger.LogError(ex, "An error occurred while deleting user");
        Console.WriteLine(ex.Message);
        throw;
    }
}

}