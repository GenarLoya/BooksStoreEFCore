using BookApi.Src.Common;
using BookApi.Src.Domain.Book.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Src.Domain.Book;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService service) : ControllerBase
{
    private readonly IBookService _service = service;

    // GET: api/Book?page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<BookModel>>> GetBooks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(page, pageSize);
        return Ok(result);
    }

    // GET: api/Book/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookModel>> GetBook(int id)
    {
        var book = await _service.GetByIdAsync(id);
        if (book is null) return NotFound();
        return Ok(book);
    }

    // POST: api/Book
    [HttpPost]
    public async Task<ActionResult<BookModel>> CreateBook(CreateBookDto dto)
    {
        var book = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT: api/Book/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    // DELETE: api/Book/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
