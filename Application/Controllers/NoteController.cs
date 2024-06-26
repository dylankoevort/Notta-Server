using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("GetAllNotes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotes();
            return Ok(notes);
        }

        [HttpGet("GetNoteById")]
        public async Task<ActionResult<Note>> GetNoteById(string userId, string noteId)
        {
            var note = await _noteService.GetNoteById(userId, noteId);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }
        
        [HttpGet("GetNotesByUserId")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByUserId([FromQuery] string userId)
        {
            var notes = await _noteService.GetNotesByUserId(userId);
            return Ok(notes);
        }
        
        [HttpGet("GetNotesByNotebookId")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByNotebookId([FromQuery] string userId, [FromQuery] string notebookId)
        {
            var notes = await _noteService.GetNotesByNotebookId(userId, notebookId);
            return Ok(notes);
        }

        [HttpPost("CreateNote")]
        public async Task<ActionResult<Note>> CreateNote(string userId, [FromBody] Note note)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            var createdNote = await _noteService.CreateNote(userId, note);
            return CreatedAtAction(nameof(GetNoteById), new { userId = userId, noteId = createdNote.NoteId }, createdNote);
        }

        [HttpPut("UpdateNote")]
        public async Task<ActionResult<Note>> UpdateNote(string userId, string noteId, [FromBody] Note note)
        {
            
            if (userId != note.UserId)
            {
                return BadRequest("User ID mismatch");
            }
            
            if (noteId != note.NoteId)
            {
                return BadRequest("Note ID mismatch");
            }

            var updatedNote = await _noteService.UpdateNote(userId, note);
            if (updatedNote == null)
            {
                return NotFound();
            }

            return Ok(updatedNote);
        }

        [HttpDelete("DeleteNote")]
        public async Task<ActionResult> DeleteNote(string userId, string noteId)
        {
            await _noteService.DeleteNote(userId, noteId);
            return NoContent();
        }
    }
}
