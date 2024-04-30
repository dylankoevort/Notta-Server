// using Infrastructure;
// using Microsoft.AspNetCore.Mvc;
// using Models;
//
// namespace Application.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// public class NoteController : ControllerBase
// {
//     private readonly INoteService _noteService;
//
//     public NoteController(INoteService noteService)
//     {
//         _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
//     }
//     
//     // GET: api/Note/GetNotes
//     [HttpGet("GetNotes")]
//     public ActionResult<IEnumerable<Note>> GetNotes()
//     {
//         try
//         {
//             var notes = _noteService.GetAllNotes();
//             return Ok(notes);
//         }
//         catch (Exception ex)
//         {
//             // Log the exception for further investigation
//             // logger.LogError(ex, "An error occurred while getting notes");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//
//     // GET: api/Note/GetNotesByUserId/{userId/userUid}
//     [HttpGet("GetNotesByUserId")]
//     public ActionResult<IEnumerable<Note>> GetNotesByUserId(int? userId, string? userUid)
//     {
//         try
//         {
//             var notes = _noteService.GetNotesByUserId(userId, userUid);
//             return Ok(notes);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting notes by user ID");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//
//     // GET: api/Note/GetNoteById/{noteId}
//     [HttpGet("GetNoteById")]
//     public ActionResult<Note> GetNoteById(int noteId)
//     {
//         try
//         {
//             var note = _noteService.GetNoteById(noteId);
//             if (note == null)
//             {
//                 return NotFound();
//             }
//             return Ok(note);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting note by ID");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//     
//     // GET: api/Note/GetNoteBySlug/{noteSlug}
//     [HttpGet("GetNoteBySlug")]
//     public ActionResult<Note> GetNoteBySlug(string noteSlug)
//     {
//         try
//         {
//             var note = _noteService.GetNoteBySlug(noteSlug);
//             if (note == null)
//             {
//                 return NotFound();
//             }
//             return Ok(note);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while getting note by slug");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//
//     // POST: api/Note/AddNote
//     [HttpPost("AddNote")]
//     public ActionResult<Note> AddNote([FromBody] Note note)
//     {
//         try
//         {
//             if (note == null)
//             {
//                 return BadRequest("Note data is missing");
//             }
//
//             _noteService.AddNote(note);
//
//             return CreatedAtAction(nameof(GetNoteById), new { noteId = note.NoteId }, note);
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while adding note");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//
//     // PUT: api/Note/UpdateNote/{noteId}
//     [HttpPut("UpdateNote")]
//     public IActionResult UpdateNote(int noteId, [FromBody] Note note)
//     {
//         try
//         {
//             if (noteId != note.NoteId)
//             {
//                 return BadRequest("Note ID mismatch");
//             }
//
//             _noteService.UpdateNote(note);
//             return Ok("Note updated successfully");
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while updating note");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
//
//     // DELETE: api/Note/DeleteNote/{noteId}
//     [HttpDelete("DeleteNote")]
//     public IActionResult DeleteNote(int noteId)
//     {
//         try
//         {
//             var note = _noteService.GetNoteById(noteId);
//             if (note == null)
//             {
//                 return NotFound("Note not found");
//             }
//
//             _noteService.DeleteNote(noteId);
//             return Ok("Note deleted successfully");
//         }
//         catch (Exception ex)
//         {
//             // logger.LogError(ex, "An error occurred while deleting note");
//             Console.WriteLine(ex.Message);
//             return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
//         }
//     }
// }
//
