using _132ndWebsite.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace _132ndWebsite.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Squadron> Squadrons => Set<Squadron>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed initial squadron data
        modelBuilder.Entity<Squadron>().HasData(
            new Squadron(1, "494th vFighter Squadron", "The Panthers"),
            new Squadron(2, "388th Fighter Squadron", "The Peregrines"),
            new Squadron(3, "335th Special Operations Squadron", "Ravens")
        );
    }
}