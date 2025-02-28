using System.Net;
using Mapster;
using Marvel.Application.Common;
using Marvel.Application.Contracts;
using Marvel.Application.DTO;
using Marvel.Domain.Entities;
using Marvel.Infraestructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Infraestructure.Implementation;


public class FavoriteRepository : IFavoriteRepository
{
    private MarvelDbContext _context;
    private IUserRepository _userRepository;

    public FavoriteRepository(MarvelDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<RequestResponse<FavoriteDTO>> AddFavoriteAsync(FavoriteDTO favoriteDTO)
    {
        var user = await _userRepository.GetUserByEmailAsync(favoriteDTO.Email);

        if(user == null)
        {
            return new RequestResponse<FavoriteDTO>()
            {
                Title = "Error al agregar favorito",
                Detail = "No existe usuario con email ingresado",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        var favoriteAdd = favoriteDTO.Adapt<FavoriteDTO, Favorite>();
        favoriteAdd.ApplicationUserId = user.Id;

        _context.Favorite.Add(favoriteAdd);
        await _context.SaveChangesAsync();

        var favorite = favoriteAdd.Adapt<Favorite, FavoriteDTO>();
        favorite.Email = user.Email;

        return new RequestResponse<FavoriteDTO>()
        {
            Title = "Se guardó favorito con éxito",
            Data =  favorite,
            StatusCode = (int)HttpStatusCode.Created
        };

    }

    public async Task<RequestResponse<List<FavoriteDTO>>> GetFavoritesByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if(user == null)
        {
            return new RequestResponse<List<FavoriteDTO>>()
            {
                Title = "Error al agregar favorito",
                Detail = "No existe usuario con email ingresado",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        var favorites =  _context.Favorite.Where(x => x.ApplicationUserId == user.Id).ToList();
        var favoritesList = favorites.Adapt<List<Favorite>, List<FavoriteDTO>>();

        return new RequestResponse<List<FavoriteDTO>>()
        {
            Title = "Consulta exitosa",
            Data = favoritesList
        };

    }

    public async Task<RequestResponse<FavoriteDTO>> RemoveFavoriteAsync(FavoriteDTO favoriteDTO)
    {
        var user = await _userRepository.GetUserByEmailAsync(favoriteDTO.Email);

        if(user == null)
        {
            return new RequestResponse<FavoriteDTO>()
            {
                Title = "Error al quitar favorito",
                Detail = "No existe usuario con email ingresado",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        var favoriteRemove = await _context.Favorite.FirstOrDefaultAsync(x => x.ComicId==favoriteDTO.ComicId && x.ApplicationUserId == user.Id);

        if(favoriteRemove == null)
        {
            return new RequestResponse<FavoriteDTO>()
            {
                Title = "Error al quitar favorito",
                Detail = "No existe favorito para el usuario y comic ingresado",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        _context.Favorite.Remove(favoriteRemove);
        await _context.SaveChangesAsync();

        var favorite = favoriteRemove.Adapt<Favorite, FavoriteDTO>();
        favorite.Email = user.Email;

        return new RequestResponse<FavoriteDTO>()
        {
            Title = "Se quitó el favorito con éxito",
            Data =  favorite,
            StatusCode = (int)HttpStatusCode.OK
        };
    }
}