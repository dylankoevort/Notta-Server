using Models;

namespace Application.Helpers;

public class NoteSlugGenerator
{
    public static string GenerateSlug(Note note)
    {
        if (note == null)
        {
            throw new ArgumentNullException(nameof(note));
        }
        
        if (string.IsNullOrEmpty(note.UserId))
        {
            throw new ArgumentNullException(nameof(note.UserId), "UserId must be provided");
        }
        
        string slug = GetLastNCharacters(note.UserId) + "-" + GenerateDateTimeString() + "-" + GenerateRandomString();
        
        slug = slug.ToLower();
        
        return slug;
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