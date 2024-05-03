using DapperSimpleProject.models;
using DapperSimpleProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperSimpleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookService.AddAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookService.UpdateAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("BooksAndAuthors")]
        public async Task<IActionResult> GetBooksAndAuthors()
        {
            var books = await _bookService.GetBooksAndAuthorsAsync();
            return Ok(books);
        }
    }
}
