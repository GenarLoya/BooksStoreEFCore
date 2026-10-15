using BookApi.Src.Common;
using BookApi.Src.Domain.Author.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Src.Domain.Author;

[ApiController]
[Route("api/[controller]")]
public class AuthorController(IAuthorService service) : ControllerBase
{
    private readonly IAuthorService _service = service;

    // GET: api/Author?page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<AuthorModel>>> GetAuthors(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(page, pageSize);
        return Ok(result);
    }

    // GET: api/Author/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorModel>> GetAuthor(int id)
    {
        var author = await _service.GetByIdAsync(id);
        if (author is null) return NotFound();
        return Ok(author);
    }

    // POST: api/Author
    [HttpPost]
    public async Task<ActionResult<AuthorModel>> CreateAuthor(CreateAuthorDto dto)
    {
        var author = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    // PUT: api/Author/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    // DELETE: api/Author/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
