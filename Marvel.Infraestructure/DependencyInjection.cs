using System.Text;
using Marvel.Application.Common;
using Marvel.Application.Contracts;
using Marvel.Application.Contracts.Auth;
using Marvel.Infraestructure.Implementation;
using Marvel.Infraestructure.Implementation.Auth;
using Marvel.Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Marvel.Infraestructure;

public static class DependencyInjection
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSettings = configuration.GetSection("DbInfo").Get<DbInfoSettings>()!;

        var connectionString = $"{configuration.GetConnectionString("SqlServerConnection")}{dbSettings.DbHost}{dbSettings.DbDatabase}{dbSettings.DbUsername}{dbSettings.DbPassword}";

        services.AddDbContext<MarvelDbContext>(opt => opt.UseSqlServer(connectionString,options => options.EnableRetryOnFailure()));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults. AuthenticationScheme;
        }).AddJwtBearer(options => options. TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8. GetBytes(configuration["Jwt:Key"]!))
        });

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IComicRepository, ComicRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();

    }
}