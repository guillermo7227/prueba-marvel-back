using Marvel.Application.Common;
using Marvel.Domain.Entities;

namespace Marvel.Application.Contracts;

public interface IComicRepository
{
    public Task<RequestResponse<ComicResponseData>> GetAllComics(int offset, string email);
}