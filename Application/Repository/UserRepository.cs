using Application;
using Infrastructure;
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
        return _context.Users.ToList();
    }

    public User GetUserById(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.UserId == userId);
    }
    
    public User GetUserById(string userUid)
    {
        return _context.Users.FirstOrDefault(u => u.UserUid.Equals(userUid));
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        var dbUser = GetUserById(user.UserId);
        dbUser.UserUid = user.UserUid;
        dbUser.UserName = user.UserName;
        dbUser.UserEmail = user.UserEmail;
        _context.SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
    
    public void DeleteUser(string userUid)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserUid.Equals(userUid));
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}