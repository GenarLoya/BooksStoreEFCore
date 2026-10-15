using System.ComponentModel.DataAnnotations;

namespace BookApi.Src.Domain.Author.Dtos;

public class CreateAuthorDto
{
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
}
