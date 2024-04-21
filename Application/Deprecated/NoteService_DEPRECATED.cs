// using Application.Helpers;
// using AutoMapper;
// using Infrastructure;
// using Models;
//
// namespace Service;
//
// public class NoteService : INoteService
// {
//     private readonly INoteRepository _noteRepository;
//     private readonly IUserService _userService;
//
//     public NoteService(INoteRepository noteRepository, IUserService userService)
//     {
//         _noteRepository = noteRepository;
//         _userService = userService;
//     }
//     
//     public IEnumerable<Note> GetAllNotes()
//     {
//         try 
//         {
//             return _noteRepository.GetAllNotes();
//         } 
//         catch (Exception ex) 
//         {
//             // logger.LogError(ex, "An error occurred while getting notes");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public IEnumerable<Note> GetNotesByUserId(int? userId, string? userUid)
//     {
//         try
//         {
//             var dbUser = _userService.GetUserById(userId, userUid);
//
//             if (dbUser == null)
//             {
//                 return Enumerable.Empty<Note>();
//             }
//
//             return _noteRepository.GetNotesByUserId(dbUser.UserId);
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
//             return _noteRepository.GetNoteById(noteId);
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
//             return _noteRepository.GetNoteBySlug(noteSlug);
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
//             if (string.IsNullOrEmpty(note.NoteTitle))
//             {
//                 note.NoteTitle = "Untitled";
//             }
//             
//             if (string.IsNullOrEmpty(note.NoteContent))
//             {
//                 note.NoteContent = "No content";
//             }
//             
//             User dbUser = _userService.GetUserById(note.UserId, note.UserUid);
//             
//             if (dbUser == null)
//             {
//                 throw new Exception("User not found");
//             }
//             
//             note.UserId = dbUser.UserId;
//             note.UserUid = dbUser.UserUid;
//             note.DateCreated = DateTime.UtcNow;
//             note.DateUpdated = DateTime.UtcNow;
//             note.NoteSlug = NoteSlugGenerator.GenerateSlug(note).ToUpper();
//                 
//             _noteRepository.AddNote(note);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while adding note");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
//
//     public void UpdateNote(Note note)
//     {
//         try
//         {
//             if (note.NoteId == 0 || note.NoteId == null)
//             {
//                 throw new ArgumentNullException(nameof(note.NoteId), "NoteId must be provided");
//             }
//             
//             var existingNote = GetNoteById(note.NoteId);
//             
//             if (existingNote == null)
//             {
//                 throw new InvalidOperationException("Note does not exist");
//             }
//             
//             if (string.IsNullOrEmpty(note.NoteTitle))
//             {
//                 note.NoteTitle = "Untitled";
//             }
//             
//             if (string.IsNullOrEmpty(note.NoteContent))
//             {
//                 note.NoteContent = "No content";
//             }
//             
//             existingNote.NoteTitle = note.NoteTitle;
//             existingNote.NoteContent = note.NoteContent;
//             existingNote.DateUpdated = DateTime.UtcNow;
//             
//             _noteRepository.UpdateNote();
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
//             _noteRepository.DeleteNote(noteId);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while deleting note");
//             Console.WriteLine(ex.Message);
//             throw;
//         }
//     }
// }