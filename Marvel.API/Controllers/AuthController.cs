using Marvel.Application.Contracts.Auth;
using Marvel.Application.DTO.Authentication;
using Marvel.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Marvel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost("login")]
    public async  Task<ActionResult<RequestResponse<AuthRequestResponseData>>> Login(LoginDTO loginDTO)
    {
        return await _authRepository.LoginUserAsync(loginDTO);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RequestResponse<AuthRequestResponseData>>> Register(RegisterDTO registerDTO)
    {
        return await _authRepository.RegisterUserAsync(registerDTO);
    }
}