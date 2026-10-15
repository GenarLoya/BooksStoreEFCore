using AuthorEntity = BookApi.Src.Domain.Author.AuthorModel;

namespace BookApi.Src.Domain.Book;

public class BookModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Year { get; set; }

    public decimal Price { get; set; }

    public int AuthorId { get; set; }

    public AuthorEntity Author { get; set; } = null!;
}
