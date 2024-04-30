// using Application;
// using Infrastructure;
// using Microsoft.EntityFrameworkCore;
// using Models;
//
// namespace Repository;
//
// public class NoteRepository : INoteRepository
// {
//     private readonly AppDbContext _context;
//
//     public NoteRepository(AppDbContext context)
//     {
//         _context = context;
//     }
//     
//     public IEnumerable<Note> GetAllNotes()
//     {
//         try
//         {
//             return _context.Notes.ToList();
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting all notes");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public IEnumerable<Note> GetNotesByUserId(int userId)
//     {
//         try
//         {
//             return _context.Notes.Where(n => n.UserId == userId).ToList();
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting notes by user ID");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public Note GetNoteById(int noteId)
//     {
//         try
//         {
//             return _context.Notes.FirstOrDefault(n => n.NoteId == noteId);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting note by ID");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//     
//     public Note GetNoteBySlug(string noteSlug)
//     {
//         try
//         {
//             return _context.Notes.FirstOrDefault(n => n.NoteSlug.ToLower().Equals(noteSlug.ToLower()));
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting note by slug");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public void AddNote(Note note)
//     {
//         try
//         {
//             _context.Notes.Add(note);
//             _context.SaveChanges();
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while adding note");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public void UpdateNote()
//     {
//         try
//         {
//             _context.SaveChanges();
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while updating note");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public void DeleteNote(int noteId)
//     {
//         try
//         {
//             var note = _context.Notes.FirstOrDefault(n => n.NoteId == noteId);
//             if (note != null)
//             {
//                 _context.Notes.Remove(note);
//                 _context.SaveChanges();
//             }
//             else
//             {
//                 throw new InvalidOperationException($"Note with ID {noteId} not found");
//             }
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while deleting note");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
// }