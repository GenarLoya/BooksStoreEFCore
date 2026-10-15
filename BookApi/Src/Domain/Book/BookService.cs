using BookApi.Data;
using BookApi.Src.Common;
using BookApi.Src.Domain.Book.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Src.Domain.Book;

public interface IBookService
{
    Task<PagedResult<BookModel>> GetPagedAsync(int page, int pageSize);
    Task<BookModel?> GetByIdAsync(int id);
    Task<BookModel> CreateAsync(CreateBookDto dto);
    Task<bool> UpdateAsync(int id, UpdateBookDto dto);
    Task<bool> DeleteAsync(int id);
}

public class BookService : IBookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

        public async Task<PagedResult<BookModel>> GetPagedAsync(int page, int pageSize)
        {
            // Sane defaults: page>=1, pageSize entre 1 y 100
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var totalItems = await _context.Books.CountAsync();

            var items = await _context.Books
                .Include(b => b.Author)
                .OrderBy(b => b.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<BookModel>(items, page, pageSize, totalItems);
        }

    public async Task<BookModel?> GetByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<BookModel> CreateAsync(CreateBookDto dto)
    {
        var book = new BookModel
        {
            Title = dto.Title,
            Year = dto.Year,
            Price = dto.Price,
            AuthorId = dto.AuthorId,
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) return false;

        book.Title = dto.Title;
        book.Year = dto.Year;
        book.Price = dto.Price;
        book.AuthorId = dto.AuthorId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}
