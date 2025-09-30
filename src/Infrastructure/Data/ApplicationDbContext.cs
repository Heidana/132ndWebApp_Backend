using _132ndWebsite.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace _132ndWebsite.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Squadron> Squadrons => Set<Squadron>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed initial squadron data
        modelBuilder.Entity<Squadron>().HasData(
            new Squadron { Id = 1, Name = "494th vFighter Squadron", Callsign = "The Panthers" },
            new Squadron { Id = 2, Name = "388th Fighter Squadron", Callsign = "The Peregrines" },
            new Squadron { Id = 3, Name = "335th Special Operations Squadron", Callsign = "Ravens" }
        );
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
        });
    }
}