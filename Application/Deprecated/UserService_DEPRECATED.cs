// using Infrastructure;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Models;
//
// namespace Service;
//
// public class UserService : IUserService
// {
//     private readonly IUserRepository _userRepository;
//
//     public UserService(IUserRepository userRepository)
//     {
//         _userRepository = userRepository;
//     }
//
//     public IEnumerable<User> GetAllUsers()
//     {
//         try
//         {
//             return _userRepository.GetAllUsers();
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting all users");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public User GetUserById(int? userId, string? userUid)
//     {
//         try
//         {
//             if ((userId == null || userId == 0) && userUid == null)
//             {
//                 throw new ArgumentNullException(nameof(userId), "userId or userUid must be provided");
//             }
//         
//             if (userId != null && userId != 0)
//             {
//                 return _userRepository.GetUserById(userId.Value);
//             }
//         
//             return _userRepository.GetUserByUid(userUid);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting user by ID");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//
//     public void AddUser(User user)
//     {
//         try
//         {
//             if (string.IsNullOrEmpty(user.UserUid))
//             {
//                 throw new ArgumentNullException(nameof(user.UserUid), "UserUid must be provided");
//             }
//         
//             if (string.IsNullOrEmpty(user.UserName))
//             {
//                 throw new ArgumentNullException(nameof(user.UserName), "UserName must be provided");
//             }
//         
//             if (string.IsNullOrEmpty(user.UserEmail))
//             {
//                 throw new ArgumentNullException(nameof(user.UserEmail), "UserEmail must be provided");
//             }
//         
//             if (GetUserById(user.UserId, user.UserUid) != null)
//             {
//                 // throw new InvalidOperationException("User already exists");
//                 return;
//             }
//         
//             _userRepository.AddUser(user);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while adding user");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public void UpdateUser(User user)
//     {
//         try
//         {
//             if (string.IsNullOrEmpty(user.UserUid))
//             {
//                 throw new ArgumentNullException(nameof(user.UserUid), "UserUid must be provided");
//             }
//
//             var dbUser = GetUserById(user.UserId, user.UserUid);
//
//             if (dbUser == null)
//             {
//                 throw new InvalidOperationException("User does not exist");
//             }
//             
//             dbUser.UserName = user.UserName;
//             dbUser.UserEmail = user.UserEmail;
//
//             _userRepository.UpdateUser();
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while updating user");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public void DeleteUser(int? userId, string? userUid)
//     {
//         try
//         {
//             if (userId == null && userUid == null)
//             {
//                 throw new ArgumentNullException(nameof(userId), "userId or userUid must be provided");
//             }
//         
//             var dbUser = GetUserById(userId, userUid);
//         
//             if (dbUser == null)
//             {
//                 throw new InvalidOperationException("User does not exist");
//             }
//         
//             _userRepository.DeleteUser(dbUser.UserId);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while deleting user");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
// }