using OneOf;

namespace Marvel.Domain.Entities;

public class Comic
{
    public int id { get; set; }
    public string title { get; set; } = null!;
    public ComicThumbnail thumbnail { get; set; } = null!;
    public List<TextObject> textObjects { get; set; } = null!;
    public List<Url> urls { get; set; } = null!;
    public bool EsFavorito { get; set; }
}

public class TextObject
{
    public string type { get; set; } = default!;
    public string text { get; set; } = default!;
}

public class Url
{
    public string type { get; set; } = default!;
    public string url { get; set; } = default!;
}

public class ComicResponse
{
    public object code { get; set; } = default!;
    public string message { get; set; } = default!;
    public ComicResponseData data { get; set; } = default!;
}

public class ComicResponseData
{
    public int count { get; set; }
    public int total { get; set; }
    public int offset { get; set; }
    public List<Comic> results { get; set; } = new List<Comic>();
}   

public record ComicThumbnail
(
    string path,
    string extension
);