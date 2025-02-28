using Marvel.Application.DTO.Authentication;
using Marvel.Application.Common;
using Marvel.Domain.Entities;

namespace Marvel.Application.Contracts.Auth;

public interface IAuthRepository
{
    Task<RequestResponse<AuthRequestResponseData>> RegisterUserAsync(RegisterDTO registerDTO);
    Task<RequestResponse<AuthRequestResponseData>> LoginUserAsync(LoginDTO loginDTO);

}