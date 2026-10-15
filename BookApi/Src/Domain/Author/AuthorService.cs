using BookApi.Data;
using BookApi.Src.Common;
using BookApi.Src.Domain.Author.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Src.Domain.Author;

public interface IAuthorService
{
    Task<PagedResult<AuthorModel>> GetPagedAsync(int page, int pageSize);
    Task<AuthorModel?> GetByIdAsync(int id);
    Task<AuthorModel> CreateAsync(CreateAuthorDto dto);
    Task<bool> UpdateAsync(int id, UpdateAuthorDto dto);
    Task<bool> DeleteAsync(int id);
}

public class AuthorService(AppDbContext context) : IAuthorService
{
    private readonly AppDbContext _context = context;

    public async Task<PagedResult<AuthorModel>> GetPagedAsync(int page, int pageSize)
    {
        // Sane defaults: page>=1, pageSize entre 1 y 100
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var totalItems = await _context.Authors.CountAsync();
        var items = await _context.Authors
            .OrderBy(a => a.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<AuthorModel>(items, page, pageSize, totalItems);
    }

    public async Task<AuthorModel?> GetByIdAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task<AuthorModel> CreateAsync(CreateAuthorDto dto)
    {
        var author = new AuthorModel
        {
            Name = dto.Name,
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<bool> UpdateAsync(int id, UpdateAuthorDto dto)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null) return false;

        author.Name = dto.Name;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null) return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }
}
