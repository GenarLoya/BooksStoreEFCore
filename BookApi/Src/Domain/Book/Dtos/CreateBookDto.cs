using System.ComponentModel.DataAnnotations;

namespace BookApi.Src.Domain.Book.Dtos;

public class CreateBookDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Range(1, 9999)]
    public int Year { get; set; }

    [Range(0.0, 99999.99)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "AuthorId must reference an existing author (>= 1).")]
    public int AuthorId { get; set; }
}
