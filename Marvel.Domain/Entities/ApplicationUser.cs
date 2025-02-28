namespace Marvel.Domain.Entities;

public class ApplicationUser
{
    public int Id { get; set; }
    public string Identificacion { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites{ get; set; } = default!;
}
