using System.ComponentModel.DataAnnotations;

namespace Marvel.Application.DTO;

public class FavoriteDTO
{
    public int Id { get; set; }
    
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public int ComicId { get; set; }

    public int ApplicationUserId { get; set; }
    public string? ThumbnailPath { get; set; }=default!;
    public string? TextDetail { get; set; }=default!;
    public string? UrlDetailPage { get; set; }=default!;
    public string? Title { get; set; }=default!;
}
