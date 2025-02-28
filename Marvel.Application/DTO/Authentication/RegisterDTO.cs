using System.ComponentModel.DataAnnotations;

namespace Marvel.Application.DTO.Authentication;

public class RegisterDTO
{
    [Required]
    public string Nombre { get; set; } = default!;

    [Required]
    public string Identificacion { get; set; } = default!;
    
    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
    
    [Required,Compare(nameof(Password))]
    public string ConfirmarPassword { get; set; } = default!;
}
