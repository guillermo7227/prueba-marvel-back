using Marvel.Application.Common;
using Marvel.Application.DTO;
using Marvel.Domain.Entities;

namespace Marvel.Application.Contracts;

public interface IFavoriteRepository
{
    public Task<RequestResponse<List<FavoriteDTO>>> GetFavoritesByEmailAsync(string email);
    public Task<RequestResponse<FavoriteDTO>> AddFavoriteAsync(FavoriteDTO favoriteDTO);
    public Task<RequestResponse<FavoriteDTO>> RemoveFavoriteAsync(FavoriteDTO favoriteDTO);
}