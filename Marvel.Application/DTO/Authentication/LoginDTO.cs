using System.ComponentModel.DataAnnotations;

namespace Marvel.Application.DTO.Authentication;

public class LoginDTO
{
    [Required, EmailAddress]
    public string Email { get; set; } = default!;
    
    [Required]
    public string Password { get; set; } = default!;
}
