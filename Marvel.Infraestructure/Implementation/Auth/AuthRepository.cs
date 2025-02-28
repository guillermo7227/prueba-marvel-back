using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Mapster;
using Marvel.Application.Contracts;
using Marvel.Application.Contracts.Auth;
using Marvel.Application.DTO;
using Marvel.Application.DTO.Authentication;
using Marvel.Application.Common;
using Marvel.Domain.Entities;
using Marvel.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Marvel.Infraestructure.Implementation.Auth;


public class AuthRepository : IAuthRepository
{
    private readonly MarvelDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthRepository(MarvelDbContext context, IConfiguration configuration, IUserRepository userRepository)
    {
        _context = context;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<RequestResponse<AuthRequestResponseData>> LoginUserAsync(LoginDTO loginDTO)
    {        
        var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);   

        if(user == null)
        {
            return new RequestResponse<AuthRequestResponseData>()
            {
                Title = "Error al iniciar sesion",
                Detail = "No existe usuario con email ingresado",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        bool passwordCorrect = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);

        if(!passwordCorrect)
        {
            return new RequestResponse<AuthRequestResponseData>()
            {
                Title = "Error al iniciar sesion",
                Detail = "Contraseña incorrecta",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        string token = GenerateJwtToken(user);
        var userDTO = user.Adapt<ApplicationUser, ApplicationUserDTO>();

        return new RequestResponse<AuthRequestResponseData>()
        {
            Title = "Inicio de sesión satisfactorio",
            Data = new AuthRequestResponseData() { User = userDTO, Token = token }
        };

    }

    public async Task<RequestResponse<AuthRequestResponseData>> RegisterUserAsync(RegisterDTO registerDTO)
    {
        var user = await _userRepository.GetUserByEmailAsync(registerDTO.Email);   

        if(user != null)
        {
            return new RequestResponse<AuthRequestResponseData>()
            {
                Title = "Error al registrar usuario",
                Detail = "Ya existe usuario con el email ingresado",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        var userAdd = registerDTO.Adapt<RegisterDTO, ApplicationUser>();
        userAdd.Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

        _context.User.Add(userAdd);
        await _context.SaveChangesAsync();

        var userDTO = userAdd.Adapt<ApplicationUser, ApplicationUserDTO>();

        return new RequestResponse<AuthRequestResponseData>()
        {
            Title = "Se creó el usuario",
            Data = new AuthRequestResponseData() { User = userDTO },
            StatusCode = (int)HttpStatusCode.Created
        };
        
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms. HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nombre!),
            new Claim(ClaimTypes.Email, user.Email!)
        };
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"], 
            audience: _configuration["Jwt:Audience"], 
            claims: userClaims,
            expires: DateTime.Now.AddHours(5), 
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token) ;
    }
}