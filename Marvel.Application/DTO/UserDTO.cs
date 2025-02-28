namespace Marvel.Application.DTO;

public class ApplicationUserDTO
{
    public int Id { get; set; }
    public string Identificacion { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
}
