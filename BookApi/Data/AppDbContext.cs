using BookApi.Src.Domain.Author;
using BookApi.Src.Domain.Book;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BookModel> Books => Set<BookModel>();
    public DbSet<AuthorModel> Authors => Set<AuthorModel>();
}
