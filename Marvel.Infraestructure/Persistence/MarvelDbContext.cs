using Marvel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Marvel.Infraestructure.Persistence;

public class MarvelDbContext : DbContext
{
    public MarvelDbContext(DbContextOptions<MarvelDbContext> Options) : base(Options)
    {
    }

    override protected void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany(au => au.Favorites)
            .HasForeignKey(f => f.ApplicationUserId);

        builder.Entity<Favorite>()
            .HasIndex(x => new { x.ApplicationUserId, x.ComicId })
            .IsUnique();

    }
    
    public DbSet<ApplicationUser> User { get; set; }
    public DbSet<Favorite> Favorite { get; set; }
}