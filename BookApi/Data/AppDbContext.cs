using BookApi.Src.Domain.Book;
using Microsoft.EntityFrameworkCore;
using BookModel = BookApi.Src.Domain.Book.Book;

namespace BookApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BookModel> Books => Set<BookModel>();
}
