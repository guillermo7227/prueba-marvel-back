using System.Net;
using System.Security.Cryptography.X509Certificates;
using Marvel.Application.DTO;
using Marvel.Domain.Entities;

namespace Marvel.Application.Common;

public class AuthRequestResponseData
{
    public ApplicationUserDTO User { get; set; } = default(ApplicationUserDTO)!;
    public string Token { get; set; } = default!;
}