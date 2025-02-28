using OneOf;

namespace Marvel.Domain.Entities;

public class Favorite
{
    public int Id { get; set; }
    public int ApplicationUserId { get; set; }
    public int ComicId { get; set; }
    public string ThumbnailPath { get; set; }=default!;
    public string TextDetail { get; set; }=default!;
    public string UrlDetailPage { get; set; }=default!;
    public string Title { get; set; }=default!;
    
    public virtual ApplicationUser User { get; set; } = default!;
}
