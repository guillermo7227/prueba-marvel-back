using Marvel.Application.Contracts;
using Marvel.Application.Contracts.Auth;
using Marvel.Application.DTO.Authentication;
using Marvel.Application.Common;
using Marvel.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Marvel.Application.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Marvel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteController(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    [HttpPost]
    public async  Task<ActionResult<RequestResponse<FavoriteDTO>>> Add(FavoriteDTO favoriteDTO)
    {
        return await _favoriteRepository.AddFavoriteAsync(favoriteDTO);
    }

    [HttpDelete]
    public async  Task<ActionResult<RequestResponse<FavoriteDTO>>> Delete(FavoriteDTO favoriteDTO)
    {
        return await _favoriteRepository.RemoveFavoriteAsync(favoriteDTO);
    }

    [HttpGet]
    public async  Task<ActionResult<RequestResponse<List<FavoriteDTO>>>> GetByEmail([FromQuery]string email)
    {
        return await _favoriteRepository.GetFavoritesByEmailAsync(email);
    }

}