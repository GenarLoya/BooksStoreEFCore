using BookApi.Data;
using BookApi.Src.Domain.Book.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Src.Domain.Book;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(books);
    }

    // GET: api/Book/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    // POST: api/Book
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Year = dto.Year,
            Price = dto.Price,
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT: api/Book/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Year = dto.Year;
        book.Price = dto.Price;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Book/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
