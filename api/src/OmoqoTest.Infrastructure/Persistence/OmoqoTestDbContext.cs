using Microsoft.EntityFrameworkCore;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Infrastructure.Persistence
{
    public class OmoqoTestDbContext : DbContext
    {
        public OmoqoTestDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            OmoqoTestDbContextSeed.SeedData(this);
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ship> Ships { get; set; } = null!;
    }
}