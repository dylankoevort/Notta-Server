using Google.Cloud.Firestore;
using Models;

namespace Application.Helpers;

public class Helpers
{
    /// <summary>
    /// Converts a Firestore timestamp object to a nullable DateTime object.
    /// </summary>
    /// <param name="timestamp">The Firestore timestamp object to convert.</param>
    /// <returns>A nullable DateTime object representing the timestamp, or null if the input is not a valid Firestore timestamp.</returns>
    public static DateTime? ConvertFirestoreTimestamp(object timestamp)
    {
        if (timestamp is Timestamp firestoreTimestamp)
        {
            return firestoreTimestamp.ToDateTime();
        }
        return null;
    }
    
    /// <summary>
    /// Converts the current UTC time to a Firestore timestamp.
    /// </summary>
    /// <returns>A Firestore timestamp representing the current UTC time.</returns>
    public static Timestamp GetCurrentFirestoreTimestamp()
    {
        return Timestamp.FromDateTime(DateTime.UtcNow);
    }
    
    public static string GenerateNewNoteId(Note note)
    {
        if (note == null)
        {
            throw new ArgumentNullException(nameof(note));
        }
        
        if (string.IsNullOrEmpty(note.UserId))
        {
            throw new ArgumentNullException(nameof(note.UserId), "UserId must be provided");
        }
        
        string id = GetLastNCharacters(note.UserId) + "-" + GenerateDateTimeString() + "-" + GenerateRandomString();
        
        id = id.ToLower();
        
        return id;
    }
    
    private static string GetLastNCharacters(string input, int numberOfCharacters = 6)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException(nameof(input));
        }
        
        if (input.Length <= numberOfCharacters)
        {
            return input;
        }
        
        return input.Substring(input.Length - numberOfCharacters);
    }
    
    private static string GenerateRandomString(int length = 6)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
    
    private static string GenerateDateTimeString()
    {
        DateTime now = DateTime.Now;
        return now.ToString("yyMMdd");
    }
}