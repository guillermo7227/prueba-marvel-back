using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Marvel.Application.Common;
using Marvel.Application.Contracts;
using Marvel.Domain.Entities;
using Marvel.Domain.Utils;
using Microsoft.Extensions.Configuration;

namespace Marvel.Infraestructure.Implementation;

public class ComicRepository : IComicRepository
{

    private readonly IConfiguration _configuration;
    private readonly IFavoriteRepository _favoriteRepository;
    public ComicRepository(IConfiguration configuration, IFavoriteRepository favoriteRepository)
    {
        _configuration = configuration;
        _favoriteRepository = favoriteRepository;
    }

    public async Task<RequestResponse<ComicResponseData>> GetAllComics(int offset, string email)
    {
        
        string fullURI = GetFullEndpointUri(offset);

        var client = new HttpClient();

        var response = await client.GetAsync(fullURI);

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var dataDes = JsonSerializer.Deserialize<ComicResponse>(jsonResponse);

        if(dataDes?.code.ToString() != "200")
        {
            return new RequestResponse<ComicResponseData>()
            {
                Title = "Error al recuperar la lista de comics",
                Detail = $"{dataDes?.code.ToString()} | {dataDes?.message}",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        // traer favoritos
        var favorites = await _favoriteRepository.GetFavoritesByEmailAsync(email);

        dataDes.data.results.ForEach(comic => {
            favorites.Data.ForEach(fav => {
                if(fav.ComicId == comic.id)
                {
                    comic.EsFavorito = true;
                }
            });
        });

        return new RequestResponse<ComicResponseData>()
        {
            Title = "Consulta exitosa",
            Data = dataDes.data
        };

    }

    private string GetFullEndpointUri(int offset = 0)
    {
        var apiSettings = _configuration.GetSection("MarvelAPI").Get<MarvelAPISettings>();
        string uriBase = apiSettings?.BaseEndpoint!;
        string comicsEndpoint = apiSettings?.ComicsEndpoint!;
        string privateKey = apiSettings?.PrivateKey!;
        string publicKey = apiSettings?.PublicKey!;
        string ts = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        string hash = Utils.CreateMD5($"{ts}{privateKey}{publicKey}");

        string fullURI = $"{uriBase}/{comicsEndpoint}?ts={ts}&apikey={publicKey}&hash={hash}&offset={offset}";

        return fullURI;
    }

}

