namespace BookApi.Src.Domain.Book;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int Year { get; set; }

    public decimal Price { get; set; }
}
