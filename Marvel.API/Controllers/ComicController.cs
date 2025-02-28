using Marvel.Application.Contracts;
using Marvel.Application.Contracts.Auth;
using Marvel.Application.DTO.Authentication;
using Marvel.Application.Common;
using Marvel.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Microsoft.AspNetCore.Authorization;

namespace Marvel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ComicController : ControllerBase
{
    private readonly IComicRepository _comicRepository;

    public ComicController(IComicRepository comicRepository)
    {
        _comicRepository = comicRepository;
    }

    [HttpGet]
    public async  Task<ActionResult<RequestResponse<ComicResponseData>>> GetAll([FromQuery]string email,[FromQuery]int offset = 0)
    {
        var result = await _comicRepository.GetAllComics(offset, email);
        return result;
    }

}