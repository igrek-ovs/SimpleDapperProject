using DapperSimpleProject.models;
using DapperSimpleProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperSimpleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authorService.AddAsync(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authorService.UpdateAsync(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var existingAuthor = await _authorService.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
