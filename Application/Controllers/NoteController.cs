using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
    }

    // GET: api/Note/GetNotesByUserId/{userId}
    [HttpGet("GetNotesByUserId")]
    public ActionResult<IEnumerable<Note>> GetNotesByUserId(int userId)
    {
        var notes = _noteService.GetNotesByUserId(userId);
        return Ok(notes);
    }

    // GET: api/Note/GetNoteById/{noteId}
    [HttpGet("GetNoteById")]
    public ActionResult<Note> GetNoteById(int noteId)
    {
        var note = _noteService.GetNoteById(noteId);
        if (note == null)
        {
            return NotFound();
        }
        return Ok(note);
    }

    // POST: api/Note/AddNote
    [HttpPost("AddNote")]
    public ActionResult<Note> AddNote([FromBody] Note note)
    {
        if (note == null)
        {
            return BadRequest();
        }

        _noteService.AddNote(note);

        return CreatedAtAction(nameof(GetNoteById), new { noteId = note.NoteId }, note);
    }

    // PUT: api/Note/UpdateNote/{noteId}
    [HttpPut("UpdateNote")]
    public IActionResult UpdateNote(int noteId, [FromBody] Note note)
    {
        if (noteId != note.NoteId)
        {
            return BadRequest();
        }

        var existingNote = _noteService.GetNoteById(noteId);
        if (existingNote == null)
        {
            return NotFound();
        }

        _noteService.UpdateNote(note);

        return Ok();
    }

    // DELETE: api/Note/DeleteNote/{noteId}
    [HttpDelete("DeleteNote")]
    public IActionResult DeleteNote(int noteId)
    {
        var note = _noteService.GetNoteById(noteId);
        if (note == null)
        {
            return NotFound();
        }

        _noteService.DeleteNote(noteId);

        return Ok();
    }
}

