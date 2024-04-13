namespace Models;

public class Note
{
    public int NoteId { get; set; }
    public int UserId { get; set; }
    public string UserUid { get; set; }
    public string NoteTitle { get; set; }
    public string NoteContent { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}