using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotebookController : ControllerBase
    {
        private readonly INotebookService _notebookService;

        public NotebookController(INotebookService notebookService)
        {
            _notebookService = notebookService;
        }

        [HttpGet("GetAllNotebooks")]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetAllNotebooks()
        {
            var notebooks = await _notebookService.GetAllNotebooks();
            return Ok(notebooks);
        }

        [HttpGet("GetNotebookById")]
        public async Task<ActionResult<Notebook>> GetNotebookById(string userId, string notebookId)
        {
            var notebook = await _notebookService.GetNotebookById(userId, notebookId);
            if (notebook == null)
            {
                return NotFound();
            }
            return Ok(notebook);
        }

        [HttpGet("GetNotebooksByUserId")]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetNotebooksByUserId([FromQuery] string userId)
        {
            var notebooks = await _notebookService.GetNotebooksByUserId(userId);
            return Ok(notebooks);
        }

        [HttpPost("CreateNotebook")]
        public async Task<ActionResult<Notebook>> CreateNotebook([FromQuery] string userId, [FromBody] Notebook notebook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdNotebook = await _notebookService.CreateNotebook(userId, notebook);
            return CreatedAtAction(nameof(GetNotebookById), new { userId = userId, notebookId = createdNotebook.NotebookId }, createdNotebook);
        }

        [HttpPut("UpdateNotebook")]
        public async Task<ActionResult<Notebook>> UpdateNotebook(string userId, string notebookId, [FromBody] Notebook notebook)
        {
            if (notebookId != notebook.NotebookId)
            {
                return BadRequest("Notebook ID mismatch");
            }

            var updatedNotebook = await _notebookService.UpdateNotebook(userId, notebook);
            if (updatedNotebook == null)
            {
                return NotFound();
            }

            return Ok(updatedNotebook);
        }

        [HttpDelete("DeleteNotebook")]
        public async Task<ActionResult> DeleteNotebook(string userId, string notebookId)
        {
            await _notebookService.DeleteNotebook(userId, notebookId);
            return NoContent();
        }
    }
}
