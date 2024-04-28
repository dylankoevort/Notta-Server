using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;
using Models;

public class UserRepository : IUserRepository
{
    private readonly FirebaseContext _firebaseContext;
    private const string CollectionName = "users";

    public UserRepository(FirebaseContext firebaseContext)
    {
        _firebaseContext = firebaseContext ?? throw new ArgumentNullException(nameof(firebaseContext));
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var query = _firebaseContext.Database.Collection(CollectionName);
        var querySnapshot = await query.GetSnapshotAsync();

        var users = new List<User>();
        foreach (var documentSnapshot in querySnapshot.Documents)
        {
            users.Add(documentSnapshot.ConvertTo<User>());
        }
        return users;
    }

    public async Task<User> GetUserById(string userId)
    {
        var document = _firebaseContext.Database.Collection(CollectionName).Document(userId.ToString());
        var documentSnapshot = await document.GetSnapshotAsync();
        return documentSnapshot.Exists ? documentSnapshot.ConvertTo<User>() : null;
    }
    
    public async Task<User> CreateUser(User user)
    {
        var collection = _firebaseContext.Database.Collection(CollectionName);
        var documentReference = collection.Document(user.UserId);
        await documentReference.SetAsync(user);
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        var document = _firebaseContext.Database.Collection(CollectionName).Document(user.UserId.ToString());
        await document.SetAsync(user, SetOptions.MergeAll);
        return user;
    }
    
    public async Task DeleteUser(string userId)
    {
        var document = _firebaseContext.Database.Collection(CollectionName).Document(userId.ToString());
        await document.DeleteAsync();
    }
}